using Domain.Entities;

namespace Interface.Persistence;

public interface IContenidoCatalogoRepository : IGenericRepository<ContenidoCatalogo>
{
    #region Metodos Sincronos

    IEnumerable<ContenidoCatalogo> GetContenidoCatalogoByCatalogoId(int id);

    #endregion

    #region Metodos asincronos

    Task<IEnumerable<ContenidoCatalogo>> GetContenidoCatalogoByCatalogoIdAsync(int id);

    #endregion
}