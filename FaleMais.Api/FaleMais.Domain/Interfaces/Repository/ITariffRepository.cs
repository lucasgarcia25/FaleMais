using FaleMais.Domain.Entities;

namespace FaleMais.Domain.Interfaces.Repository
{
    public interface ITariffRepository
    {
        Tariff? GetTariff(string origin, string destination);
    }
}
