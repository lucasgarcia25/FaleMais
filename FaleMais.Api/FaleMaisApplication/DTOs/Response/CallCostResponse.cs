namespace FaleMaisApplication.DTOs.Response
{
    public class CallCostResponse
    {
        public decimal CostWithPlan { get; private set; }
        public decimal CostWithoutPlan { get; private set; }

        public CallCostResponse(decimal costWithPlan, decimal costWithoutPlan)
        {
            CostWithPlan = costWithPlan;
            CostWithoutPlan = costWithoutPlan;
        }
    }
}
