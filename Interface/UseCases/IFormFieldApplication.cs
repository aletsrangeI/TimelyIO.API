using Common;
using DTO.FormField;

namespace Interface.UseCases;

public interface IFormFieldApplication
{
    #region Metodos sincronos

    Response<bool> Insert(FormFieldDTO formField);
    Response<bool> Update(FormFieldDTO formField);
    Response<bool> Delete(int id);
    Response<FormFieldDTO> Get(int id);
    Response<IEnumerable<FormFieldDTO>> GetAll();
    ResponsePagination<IEnumerable<FormFieldDTO>> GetAllWithPagination(int page, int pageSize);
    Response<int> Count();
    Response<IEnumerable<FormFieldDTO>> GetFormFieldByFormCatId(int id);

    #endregion

    #region Metodos asincronos

    Task<Response<bool>> InsertAsync(FormFieldDTO formField);
    Task<Response<bool>> UpdateAsync(FormFieldDTO formField);
    Task<Response<bool>> DeleteAsync(int id);
    Task<Response<FormFieldDTO>> GetAsync(int id);
    Task<Response<IEnumerable<FormFieldDTO>>> GetAllAsync();
    Task<ResponsePagination<IEnumerable<FormFieldDTO>>> GetAllWithPaginationAsync(int page, int pageSize);
    Task<Response<int>> CountAsync();
    Task<Response<IEnumerable<FormFieldDTO>>> GetFormFieldByFormCatIdAsync(int id);

    #endregion
}