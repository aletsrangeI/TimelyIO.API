using Domain.Entities;

namespace Interface.Persistence;

/// <summary>
/// Contrato de persistencia para ExpedienteCliente.
/// Hereda de IGenericRepository pero promueve metodos asincronos.
/// </summary>
public interface IExpedienteClienteRepository : IGenericRepository<ExpedienteCliente>
{
}
