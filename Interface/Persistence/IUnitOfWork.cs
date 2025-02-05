namespace Interface.Persistence;

public interface IUnitOfWork : IDisposable
{
    ICatalogoRepository Catalogos { get; }
    IContenidoCatalogoRepository ContenidoCatalogos { get; }
    IFormFieldRepository FormFields { get; }
    
    Task<int> Save(CancellationToken cancellationToken);
}