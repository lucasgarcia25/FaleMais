using FaleMais.Domain.Interfaces.Repository;
using FaleMais.Infrastructure.Repositories;

namespace FaleMais.Api.Factories
{
    public static class DatabaseFactory
    {
        public static ITariffRepository CreateRepository(IConfiguration configuration, string provider)
        {

            string connectionString = configuration["DatabaseSettings:ConnectionString"]
                ?? throw new Exception("Connection string is not configured.");

            return provider switch
            {
                "Mock" => new TariffRepositoryMock(),
                "SQLite" => new TariffRepositorySQLite(connectionString),
                _ => throw new Exception("Unsupported database. Use 'Mock' or 'SQLite'.")
            };
        }
    }
}
