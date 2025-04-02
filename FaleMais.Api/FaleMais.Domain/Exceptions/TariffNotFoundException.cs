namespace FaleMais.Domain.Exceptions
{
    public class TariffNotFoundException : Exception
    {
        public TariffNotFoundException(string origin, string destination)
            : base($"Tarifa não encontrada para a origem: {origin} e o destino: {destination}")
        {
        }
    }
}
