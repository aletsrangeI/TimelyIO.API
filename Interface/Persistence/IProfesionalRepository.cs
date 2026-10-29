using Domain.Entities;

namespace Interface.Persistence;

/// <summary>
/// Contrato de persistencia para Profesional.
/// Hereda de IGenericRepository pero promueve metodos asincronos.
/// </summary>
public interface IProfesionalRepository : IGenericRepository<Profesional>
{
}
