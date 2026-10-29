using Domain.Entities;

namespace Interface.Persistence;

/// <summary>
/// Contrato de persistencia para Cita.
/// Hereda de IGenericRepository pero promueve metodos asincronos.
/// </summary>
public interface ICitaRepository : IGenericRepository<Cita>
{
}
