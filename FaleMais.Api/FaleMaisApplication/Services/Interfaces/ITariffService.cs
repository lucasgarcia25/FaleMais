using FaleMais.Domain.Enums;
using FaleMaisApplication.DTOs.Response;

namespace FaleMaisApplication.Services.Interfaces
{
    public interface ITariffService
    {
        Result<CallCostResponse> GetCallCostResponse(string origin, string destination, int duration, PlanType planType);
    }
}
