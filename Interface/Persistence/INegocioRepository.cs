using Domain.Entities;

namespace Interface.Persistence;

/// <summary>
/// Contrato de persistencia para Negocio.
/// Hereda de IGenericRepository pero promueve metodos asincronos.
/// </summary>
public interface INegocioRepository : IGenericRepository<Negocio>
{
}
