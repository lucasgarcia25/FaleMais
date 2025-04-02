using FaleMais.Api.Controllers;
using FaleMais.Domain.Enums;
using FaleMaisApplication.DTOs.Request;
using FaleMaisApplication.DTOs.Response;
using FaleMaisApplication.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;

namespace FaleMais.Tests.Controllers
{
    public class TariffControllerTests
    {
        private readonly Mock<ITariffService> _tariffServiceMock;
        private readonly TariffController _controller;
        private readonly Mock<ILogger<TariffController>> _loggerMock;

        public TariffControllerTests()
        {
            _tariffServiceMock = new Mock<ITariffService>();
            _loggerMock = new Mock<ILogger<TariffController>>();

            _controller = new TariffController(_tariffServiceMock.Object, _loggerMock.Object);
        }

        [Fact]
        public void Calculate_ReturnsOkResult_WhenSuccess()
        {
            CallCostRequest callCostRequest = new CallCostRequest
            {
                Origin = "011",
                Destination = "016",
                Duration = 30,
                PlanType = PlanType.FALEMAIS30
            };

            CallCostResponse callCostResponse = new CallCostResponse(10.0m, 20.0m);
            Result<CallCostResponse> result = Result<CallCostResponse>.SuccessResult(callCostResponse);

            _tariffServiceMock.Setup(service => service.GetCallCostResponse(
                callCostRequest.Origin, callCostRequest.Destination, callCostRequest.Duration, callCostRequest.PlanType))
                .Returns(result);

            IActionResult actionResult = _controller.Calculate(callCostRequest);

            OkObjectResult okResult = Assert.IsType<OkObjectResult>(actionResult);
            Result<CallCostResponse> returnValue = Assert.IsType<Result<CallCostResponse>>(okResult.Value);
            Assert.True(returnValue.Success);
            Assert.Equal(callCostResponse, returnValue.Data);

            _tariffServiceMock.Verify(service => service.GetCallCostResponse(
                callCostRequest.Origin, callCostRequest.Destination, callCostRequest.Duration, callCostRequest.PlanType), Times.Once);
        }

        [Fact]
        public void Calculate_ReturnsBadRequest_WhenError()
        {
            CallCostRequest callCostRequest = new CallCostRequest
            {
                Origin = "011",
                Destination = "999",
                Duration = 30,
                PlanType = PlanType.FALEMAIS30
            };

            Result<CallCostResponse> result = Result<CallCostResponse>.ErrorResult("Tarifa não encontrada para a origem: 011 e destino: 999");

            _tariffServiceMock.Setup(service => service.GetCallCostResponse(
                callCostRequest.Origin, callCostRequest.Destination, callCostRequest.Duration, callCostRequest.PlanType))
                .Returns(result);

            IActionResult actionResult = _controller.Calculate(callCostRequest);

            BadRequestObjectResult badRequestResult = Assert.IsType<BadRequestObjectResult>(actionResult);
            Result<CallCostResponse> returnValue = Assert.IsType<Result<CallCostResponse>>(badRequestResult.Value);
            Assert.False(returnValue.Success);
            Assert.Equal("Tarifa não encontrada para a origem: 011 e destino: 999", returnValue.ErrorMessage);

            _tariffServiceMock.Verify(service => service.GetCallCostResponse(
                callCostRequest.Origin, callCostRequest.Destination, callCostRequest.Duration, callCostRequest.PlanType), Times.Once);
        }

        [Fact]
        public void Calculate_ReturnsInternalServerError_WhenExceptionThrown()
        {
            CallCostRequest callCostRequest = new CallCostRequest
            {
                Origin = "011",
                Destination = "016",
                Duration = 30,
                PlanType = PlanType.FALEMAIS30
            };

            _tariffServiceMock.Setup(service => service.GetCallCostResponse(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<int>(), It.IsAny<PlanType>()))
                              .Throws(new Exception("Erro inesperado"));

            IActionResult actionResult = _controller.Calculate(callCostRequest);

            ObjectResult internalServerErrorResult = Assert.IsType<ObjectResult>(actionResult);
            Assert.Equal(500, internalServerErrorResult.StatusCode);
            Assert.Contains("Erro inesperado", internalServerErrorResult.Value.ToString());
        }

    }
}
