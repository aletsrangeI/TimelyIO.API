using Common;
using DTO.ContenidoCatalogo;

namespace Interface.UseCases;

public interface IContenidoCatalogoApplication
{
    #region Metodos sincronos

    Response<bool> Insert(ContenidoCatalogoDTO contenidoCatalogoDto);

    Response<bool> Update(ContenidoCatalogoDTO contenidoCatalogoDto);

    Response<bool> Delete(int id);

    Response<ContenidoCatalogoDTO> Get(int id);

    Response<IEnumerable<ContenidoCatalogoDTO>> getAll();

    ResponsePagination<IEnumerable<ContenidoCatalogoDTO>> GetAllWithPagination(int page, int pageSize);

    Response<int> Count();

    Response<IEnumerable<ContenidoCatalogoDTO>> GetContenidoCatalogoByCatalogoId(int id);

    #endregion

    #region Metodos asincronos

    Task<Response<bool>> InsertAsync(ContenidoCatalogoDTO contenidoCatalogoDto);

    Task<Response<bool>> UpdateAsync(ContenidoCatalogoDTO contenidoCatalogoDto);

    Task<Response<bool>> DeleteAsync(int id);

    Task<Response<ContenidoCatalogoDTO>> GetAsync(int id);

    Task<Response<IEnumerable<ContenidoCatalogoDTO>>> GetAllAsync();

    Task<ResponsePagination<IEnumerable<ContenidoCatalogoDTO>>> GetAllWithPaginationAsync(int page, int pageSize);

    Task<Response<int>> CountAsync();

    Task<Response<IEnumerable<ContenidoCatalogoDTO>>> GetContenidoCatalogoByCatalogoIdAsync(int id);

    #endregion
}