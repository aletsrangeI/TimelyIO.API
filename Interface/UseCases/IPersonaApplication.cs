using Common;
using DTO.Persona;

namespace Interface.UseCases;

public interface IPersonaApplication
{
    #region Metodos sincronos
    
    Response<bool> Insert(PersonaDTO Persona);
    
    Response<bool> Update(PersonaDTO Persona);
    
    Response<bool> Delete(int id);
    
    Response<PersonaDTO> Get(int id);
    
    Response<IEnumerable<PersonaDTO>> GetAll();
    
    ResponsePagination<IEnumerable<PersonaDTO>> GetAllWithPagination(int page, int pageSize);
    
    Response<int> Count();
    
    Response<PersonaDTO> Authenticate(PersonaLoginDTO personaLogin);

    #endregion
    
    #region Metodos asincronos
    
    Task<Response<bool>> InsertAsync(PersonaDTO Persona);
    
    Task<Response<bool>> UpdateAsync(PersonaDTO Persona);
    
    Task<Response<bool>> DeleteAsync(int id);
    
    Task<Response<PersonaDTO>> GetAsync(int id);
    
    Task<Response<IEnumerable<PersonaDTO>>> GetAllAsync();
    
    Task<ResponsePagination<IEnumerable<PersonaDTO>>> GetAllWithPaginationAsync(int page, int pageSize);
    
    Task<Response<int>> CountAsync();
    
    Task<Response<PersonaDTO>> AuthenticateAsync(PersonaLoginDTO personaLogin);
    
    #endregion
}