using FaleMais.Domain.Entities;
using FaleMais.Domain.Interfaces.Repository;
using Microsoft.Data.Sqlite;
using SQLitePCL;

namespace FaleMais.Infrastructure.Repositories
{
    public class TariffRepositorySQLite : ITariffRepository
    {
        private readonly List<Tariff> _tariffs;

        private readonly string _connectionString;

        public TariffRepositorySQLite(string connectionString)
        {
            Batteries.Init();

            _connectionString = connectionString;
            CreateDatabase();
        }

        private void CreateDatabase()
        {
            if (!File.Exists("falemais.db"))
            {
                using var connection = new SqliteConnection(_connectionString);
                connection.Open();

                string sql = @"
                CREATE TABLE IF NOT EXISTS Tarifa (
                    Id INTEGER PRIMARY KEY AUTOINCREMENT, 
                    Origin TEXT, 
                    Destination TEXT, 
                    PricePerMinute DECIMAL(5,2)
                );

                INSERT INTO Tarifa (Origin, Destination, PricePerMinute) 
                VALUES 
                ('011', '016', 1.90),
                ('016', '011', 2.90),
                ('11', '17', 1.70),
                ('17', '11', 2.70),
                ('11', '18', 0.90),
                ('18', '11', 1.90);
                ";

                using var command = new SqliteCommand(sql, connection);
                command.ExecuteNonQuery();
            }
        }

        public Tariff? GetTariff(string origin, string destination)
        {
            using var connection = new SqliteConnection(_connectionString);
            connection.Open();

            using var command = new SqliteCommand("SELECT Origin, Destination, PricePerMinute FROM Tarifa WHERE Origin = @origin AND Destination = @destination", connection);
            command.Parameters.AddWithValue("@origin", origin);
            command.Parameters.AddWithValue("@destination", destination);

            using var reader = command.ExecuteReader();

            if (reader.Read())
            {
                return new Tariff
                {
                    Origin = reader.GetString(reader.GetOrdinal("Origin")),
                    Destination = reader.GetString(reader.GetOrdinal("Destination")),
                    PricePerMinute = reader.GetDecimal(reader.GetOrdinal("PricePerMinute"))
                };
            }

            return null;
        }
    }
}
