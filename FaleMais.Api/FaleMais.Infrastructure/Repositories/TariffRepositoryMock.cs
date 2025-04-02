using FaleMais.Domain.Entities;
using FaleMais.Domain.Exceptions;
using FaleMais.Domain.Interfaces.Repository;

namespace FaleMais.Infrastructure.Repositories
{
    public class TariffRepositoryMock : ITariffRepository
    {
        private readonly List<Tariff> _tariffs;

        public TariffRepositoryMock()
        {
            _tariffs = new List<Tariff>
            {
                new Tariff { Origin = "011", Destination = "016", PricePerMinute = 1.90m },
                new Tariff { Origin = "016", Destination = "011", PricePerMinute = 2.90m },
                new Tariff { Origin = "011", Destination = "017", PricePerMinute = 1.70m },
                new Tariff { Origin = "017", Destination = "011", PricePerMinute = 2.70m },
                new Tariff { Origin = "011", Destination = "018", PricePerMinute = 0.90m },
                new Tariff { Origin = "018", Destination = "011", PricePerMinute = 1.90m }
            };
        }

        public Tariff? GetTariff(string origin, string destination)
        {
            var tariff = _tariffs.FirstOrDefault(t => t.Origin == origin && t.Destination == destination);
            if (tariff == null)
            {
                throw new TariffNotFoundException(origin, destination);
            }
            return tariff;
        }
    }
}
