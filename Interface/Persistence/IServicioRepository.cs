using Domain.Entities;

namespace Interface.Persistence;

/// <summary>
/// Contrato de persistencia para Servicio.
/// Hereda de IGenericRepository pero promueve metodos asincronos.
/// </summary>
public interface IServicioRepository : IGenericRepository<Servicio>
{
}
