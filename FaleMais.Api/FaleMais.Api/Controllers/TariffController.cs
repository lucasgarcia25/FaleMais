using FaleMaisApplication.DTOs.Request;
using FaleMaisApplication.DTOs.Response;
using FaleMaisApplication.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace FaleMais.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TariffController : ControllerBase
    {
        private readonly ITariffService _tariffService;
        private readonly ILogger<TariffController> _logger;

        public TariffController(ITariffService tariffService, ILogger<TariffController> logger)
        {
            _tariffService = tariffService;
            _logger = logger;
        }

        [HttpPost("calculate")]
        public IActionResult Calculate(CallCostRequest request)
        {
            try
            {
                _logger.LogInformation($"CallCostRequest ran => {request.ToString()}");

                var result = _tariffService.GetCallCostResponse(request.Origin, request.Destination, request.Duration, request.PlanType);
                if (!result.Success)
                {
                    return BadRequest(result);
                }
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogInformation($"CallCostRequest ran error => {ex.Message}");

                return StatusCode(500, $"Erro inesperado: {ex.Message}");
            }
        }
    }
}