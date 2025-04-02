namespace FaleMais.Domain.Entities
{
    public class Tariff
    {
        public string Origin { get; set; }
        public string Destination { get; set; }
        public decimal PricePerMinute { get; set; }
    }
}
