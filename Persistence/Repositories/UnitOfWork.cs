using Interface.Persistence;
using Persistence.Context;

namespace Persistence.Repositories;

public class UnitOfWork : IUnitOfWork
{
    public ICatalogoRepository Catalogos { get; }
    public IContenidoCatalogoRepository ContenidoCatalogos { get; }
    public IFormFieldRepository FormFields { get; }
    public IPersonaRepository Personas { get; }
    public INegocioRepository Negocios { get; }
    public IProfesionalRepository Profesionales { get; }
    public IServicioRepository Servicios { get; }
    public IClienteRepository Clientes { get; }
    public ICitaRepository Citas { get; }
    public IExpedienteClienteRepository ExpedientesClientes { get; }

    private readonly ApplicationDbContext _dbContext;

    public UnitOfWork(
        ApplicationDbContext dbContext,
        ICatalogoRepository catalogoRepository,
        IContenidoCatalogoRepository contenidoCatalogoRepository,
        IFormFieldRepository formFieldRepository,
        IPersonaRepository personaRepository,
        INegocioRepository negocioRepository,
        IProfesionalRepository profesionalRepository,
        IServicioRepository servicioRepository,
        IClienteRepository clienteRepository,
        ICitaRepository citaRepository,
        IExpedienteClienteRepository expedienteClienteRepository)
    {
        _dbContext = dbContext;
        Catalogos = catalogoRepository;
        ContenidoCatalogos = contenidoCatalogoRepository;
        FormFields = formFieldRepository;
        Personas = personaRepository;
        Negocios = negocioRepository;
        Profesionales = profesionalRepository;
        Servicios = servicioRepository;
        Clientes = clienteRepository;
        Citas = citaRepository;
        ExpedientesClientes = expedienteClienteRepository;
    }

    public async Task<int> Save(CancellationToken cancellationToken)
    {
        return await _dbContext.SaveChangesAsync(cancellationToken);
    }

    public void Dispose()
    {
        GC.SuppressFinalize(this);
    }
}