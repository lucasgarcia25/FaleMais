using FaleMais.Domain.Entities;
using FaleMais.Domain.Enums;
using FaleMais.Domain.Exceptions;
using FaleMais.Domain.Interfaces.Repository;
using FaleMaisApplication.DTOs.Response;
using FaleMaisApplication.Services.Interfaces;
using System.Reflection.Metadata;

namespace FaleMais.Application.Services
{
    public class TariffService : ITariffService
    {
        private readonly ITariffRepository _tariffRepository;
        public TariffService(ITariffRepository tariffRepository)
        {
            _tariffRepository = tariffRepository;
        }

        public Result<CallCostResponse> GetCallCostResponse(string origin, string destination, int duration, PlanType planType)
        {
            try
            {
                decimal costWithPlan = CalculateCost(origin, destination, duration, planType);
                decimal costWithoutPlan = CalculateCostWithoutPlan(origin, destination, duration);
                CallCostResponse response = new CallCostResponse(costWithPlan, costWithoutPlan);

                return Result<CallCostResponse>.SuccessResult(response);
            }
            catch (TariffNotFoundException ex)
            {
                return Result<CallCostResponse>.ErrorResult(ex.Message);

            }
        }

        private decimal CalculateCost(string origin, string destination, int duration, PlanType planType)
        {
            Tariff? tariff = _tariffRepository.GetTariff(origin, destination);
            if (tariff == null) return 0;

            int freeMinutes = (int)planType;
            int chargeableMinutes = Math.Max(0, duration - freeMinutes);
            return chargeableMinutes * tariff.PricePerMinute * 1.1m;
        }

        private decimal CalculateCostWithoutPlan(string origin, string destination, int duration)
        {
            Tariff? tariff = _tariffRepository.GetTariff(origin, destination);
            if (tariff == null) return 0;

            return duration * tariff.PricePerMinute;
        }
    }
}