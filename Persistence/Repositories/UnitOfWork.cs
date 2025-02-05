using Interface.Persistence;
using Persistence.Context;

namespace Persistence.Repositories;

public class UnitOfWork : IUnitOfWork
{
    public ICatalogoRepository Catalogos { get; }
    public IContenidoCatalogoRepository ContenidoCatalogos { get; }

    public IFormFieldRepository FormFields { get; }

    private readonly ApplicationDbContext _dbContext;

    public UnitOfWork(ApplicationDbContext dbContext, ICatalogoRepository catalogoRepository,
        IContenidoCatalogoRepository contenidoCatalogoRepository, IFormFieldRepository formFieldRepository)
    {
        _dbContext = dbContext;
        Catalogos = catalogoRepository;
        ContenidoCatalogos = contenidoCatalogoRepository;
        FormFields = formFieldRepository;
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