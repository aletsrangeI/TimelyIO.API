using Domain.Entities;

namespace Interface.Persistence;

/// <summary>
/// Contrato de persistencia para Cliente.
/// Hereda de IGenericRepository pero promueve metodos asincronos.
/// </summary>
public interface IClienteRepository : IGenericRepository<Cliente>
{
}
