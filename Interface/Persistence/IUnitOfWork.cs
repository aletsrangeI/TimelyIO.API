namespace Interface.Persistence;

public interface IUnitOfWork : IDisposable
{
    ICatalogoRepository Catalogos { get; }
    IContenidoCatalogoRepository ContenidoCatalogos { get; }
    IFormFieldRepository FormFields { get; }
    IPersonaRepository Personas { get; }
    INegocioRepository Negocios { get; }
    IProfesionalRepository Profesionales { get; }
    IServicioRepository Servicios { get; }
    IClienteRepository Clientes { get; }
    ICitaRepository Citas { get; }
    IExpedienteClienteRepository ExpedientesClientes { get; }

    Task<int> Save(CancellationToken cancellationToken);
}