using Interface.Persistence;
using Persistence.Context;

namespace Persistence.Repositories;

public class UnitOfWork : IUnitOfWork
{
    public ICatalogoRepository Catalogos { get; }
    public IContenidoCatalogoRepository ContenidoCatalogos { get; }
    public IFormFieldRepository FormFields { get; }
    public IPersonaRepository Personas { get; }
    

    private readonly ApplicationDbContext _dbContext;

    public UnitOfWork(ApplicationDbContext dbContext, ICatalogoRepository catalogoRepository,
        IContenidoCatalogoRepository contenidoCatalogoRepository, IFormFieldRepository formFieldRepository, IPersonaRepository personaRepository)
    {
        _dbContext = dbContext;
        Catalogos = catalogoRepository;
        ContenidoCatalogos = contenidoCatalogoRepository;
        FormFields = formFieldRepository;
        Personas = personaRepository;
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