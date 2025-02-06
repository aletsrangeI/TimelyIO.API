namespace Interface.Persistence;

public interface IUnitOfWork : IDisposable
{
    ICatalogoRepository Catalogos { get; }
    IContenidoCatalogoRepository ContenidoCatalogos { get; }
    IFormFieldRepository FormFields { get; }
    IPersonaRepository Personas { get; }

    Task<int> Save(CancellationToken cancellationToken);
}