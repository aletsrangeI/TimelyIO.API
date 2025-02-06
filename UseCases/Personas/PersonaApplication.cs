using AutoMapper;
using Common;
using Domain.Entities;
using DTO.Persona;
using Interface.Persistence;
using Interface.UseCases;
using Validator;

namespace UseCases.Personas;

public class PersonaApplication : IPersonaApplication
{
    private readonly IAppLogger<PersonaApplication> _logger;
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;
    private readonly PersonaLoginDTOValidator _loginValidationRules;
    private readonly PersonaDTOValidator _personaValidationRules;

    public PersonaApplication(IAppLogger<PersonaApplication> logger, IMapper mapper, IUnitOfWork unitOfWork,
        PersonaLoginDTOValidator loginValidationRules, PersonaDTOValidator personaValidationRules)
    {
        _logger = logger;
        _mapper = mapper;
        _unitOfWork = unitOfWork;
        _loginValidationRules = loginValidationRules;
        _personaValidationRules = personaValidationRules;
    }

    #region Metodos sincronos

    public Response<bool> Insert(PersonaDTO personaDto)
    {
        var response = new Response<bool>();

        try
        {
            var persona = _mapper.Map<Persona>(personaDto);
            var validation = _personaValidationRules.Validate(personaDto);

            if (!validation.IsValid)
            {
                response.Message = validation.ToString();
                return response;
            }

            response.Data = _unitOfWork.Personas.Insert(persona);

            if (response.Data)
            {
                response.isSuccess = true;
                response.Message = "Persona insertada correctamente";
            }
        }
        catch (Exception e)
        {
            response.Message = e.Message;
            _logger.LogError(e.Message);
        }

        return response;
    }

    public Response<bool> Update(PersonaDTO personaDto)
    {
        var response = new Response<bool>();

        try
        {
            var persona = _mapper.Map<Persona>(personaDto);
            var validation = _personaValidationRules.Validate(personaDto);

            if (!validation.IsValid)
            {
                response.Message = validation.ToString();
                return response;
            }

            response.Data = _unitOfWork.Personas.Update(persona);

            if (response.Data)
            {
                response.isSuccess = true;
                response.Message = "Persona actualizada correctamente";
            }
        }
        catch (Exception e)
        {
            response.Message = e.Message;
            _logger.LogError(e.Message);
        }

        return response;
    }

    public Response<bool> Delete(int id)
    {
        var response = new Response<bool>();

        try
        {
            response.Data = _unitOfWork.Personas.Delete(id);

            if (response.Data)
            {
                response.isSuccess = true;
                response.Message = "Persona eliminada correctamente";
            }
        }
        catch (Exception e)
        {
            response.Message = e.Message;
            _logger.LogError(e.Message);
        }

        return response;
    }

    public Response<PersonaDTO> Get(int id)
    {
        var response = new Response<PersonaDTO>();

        try
        {
            var persona = _unitOfWork.Personas.Get(id);

            if (persona != null)
            {
                response.Data = _mapper.Map<PersonaDTO>(persona);
                response.isSuccess = true;
                response.Message = "Persona encontrada";
            }
            else
            {
                response.Message = "Persona no encontrada";
            }
        }
        catch (Exception e)
        {
            response.Message = e.Message;
            _logger.LogError(e.Message);
        }

        return response;
    }

    public Response<IEnumerable<PersonaDTO>> GetAll()
    {
        var response = new Response<IEnumerable<PersonaDTO>>();

        try
        {
            var personas = _unitOfWork.Personas.GetAll();

            if (personas != null)
            {
                response.Data = _mapper.Map<IEnumerable<PersonaDTO>>(personas);
                response.isSuccess = true;
                response.Message = "Personas encontradas";
            }
            else
            {
                response.Message = "No se encontraron personas";
            }
        }
        catch (Exception e)
        {
            response.Message = e.Message;
            _logger.LogError(e.Message);
        }

        return response;
    }

    public ResponsePagination<IEnumerable<PersonaDTO>> GetAllWithPagination(int page, int pageSize)
    {
        var response = new ResponsePagination<IEnumerable<PersonaDTO>>();

        try
        {
            var personas = _unitOfWork.Personas.GetAllWithPagination(page, pageSize);

            if (personas != null)
            {
                response.Data = _mapper.Map<IEnumerable<PersonaDTO>>(personas);
                response.isSuccess = true;
                response.Message = "Personas encontradas";
            }
            else
            {
                response.Message = "No se encontraron personas";
            }
        }
        catch (Exception e)
        {
            response.Message = e.Message;
            _logger.LogError(e.Message);
        }

        return response;
    }

    public Response<int> Count()
    {
        var response = new Response<int>();

        try
        {
            response.Data = _unitOfWork.Personas.Count();

            if (response.Data > 0)
            {
                response.isSuccess = true;
                response.Message = "Personas encontradas";
            }
            else
            {
                response.Message = "No se encontraron personas";
            }
        }
        catch (Exception e)
        {
            response.Message = e.Message;
            _logger.LogError(e.Message);
        }

        return response;
    }

    public Response<PersonaDTO> Authenticate(PersonaLoginDTO personaLogin)
    {
        var response = new Response<PersonaDTO>();

        try
        {
            var validation = _loginValidationRules.Validate(personaLogin);

            if (!validation.IsValid)
            {
                response.Message = validation.ToString();
                return response;
            }

            var persona = _unitOfWork.Personas.Authenticate(personaLogin);

            if (persona != null)
            {
                response.Data = _mapper.Map<PersonaDTO>(persona);
                response.isSuccess = true;
                response.Message = "Persona autenticada";
            }
            else
            {
                response.Message = "Credenciales incorrectas";
            }
        }
        catch (Exception e)
        {
            response.Message = e.Message;
            _logger.LogError(e.Message);
        }

        return response;
    }

    #endregion

    #region Metodos asincronos

    public async Task<Response<bool>> InsertAsync(PersonaDTO personaDto)
    {
        var response = new Response<bool>();

        try
        {
            var persona = _mapper.Map<Persona>(personaDto);
            var validation = _personaValidationRules.Validate(personaDto);

            if (!validation.IsValid)
            {
                response.Message = validation.ToString();
                return response;
            }

            response.Data = await _unitOfWork.Personas.InsertAsync(persona);

            if (response.Data)
            {
                response.isSuccess = true;
                response.Message = "Persona insertada correctamente";
            }
        }
        catch (Exception e)
        {
            response.Message = e.Message;
            _logger.LogError(e.Message);
        }

        return response;
    }

    public async Task<Response<bool>> UpdateAsync(PersonaDTO personaDto)
    {
        var response = new Response<bool>();

        try
        {
            var persona = _mapper.Map<Persona>(personaDto);
            var validation = _personaValidationRules.Validate(personaDto);

            if (!validation.IsValid)
            {
                response.Message = validation.ToString();
                return response;
            }

            response.Data = await _unitOfWork.Personas.UpdateAsync(persona);

            if (response.Data)
            {
                response.isSuccess = true;
                response.Message = "Persona actualizada correctamente";
            }
        }
        catch (Exception e)
        {
            response.Message = e.Message;
            _logger.LogError(e.Message);
        }

        return response;
    }

    public async Task<Response<bool>> DeleteAsync(int id)
    {
        var response = new Response<bool>();

        try
        {
            response.Data = await _unitOfWork.Personas.DeleteAsync(id);

            if (response.Data)
            {
                response.isSuccess = true;
                response.Message = "Persona eliminada correctamente";
            }
        }
        catch (Exception e)
        {
            response.Message = e.Message;
            _logger.LogError(e.Message);
        }

        return response;
    }

    public async Task<Response<PersonaDTO>> GetAsync(int id)
    {
        var response = new Response<PersonaDTO>();

        try
        {
            var persona = await _unitOfWork.Personas.GetAsync(id);

            if (persona != null)
            {
                response.Data = _mapper.Map<PersonaDTO>(persona);
                response.isSuccess = true;
                response.Message = "Persona encontrada";
            }
            else
            {
                response.Message = "Persona no encontrada";
            }
        }
        catch (Exception e)
        {
            response.Message = e.Message;
            _logger.LogError(e.Message);
        }

        return response;
    }

    public async Task<Response<IEnumerable<PersonaDTO>>> GetAllAsync()
    {
        var response = new Response<IEnumerable<PersonaDTO>>();

        try
        {
            var personas = await _unitOfWork.Personas.GetAllAsync();

            if (personas != null)
            {
                response.Data = _mapper.Map<IEnumerable<PersonaDTO>>(personas);
                response.isSuccess = true;
                response.Message = "Personas encontradas";
            }
            else
            {
                response.Message = "No se encontraron personas";
            }
        }
        catch (Exception e)
        {
            response.Message = e.Message;
            _logger.LogError(e.Message);
        }

        return response;
    }

    public async Task<ResponsePagination<IEnumerable<PersonaDTO>>> GetAllWithPaginationAsync(int page, int pageSize)
    {
        var response = new ResponsePagination<IEnumerable<PersonaDTO>>();

        try
        {
            var personas = await _unitOfWork.Personas.GetAllWithPaginationAsync(page, pageSize);

            if (personas != null)
            {
                response.Data = _mapper.Map<IEnumerable<PersonaDTO>>(personas);
                response.isSuccess = true;
                response.Message = "Personas encontradas";
            }
            else
            {
                response.Message = "No se encontraron personas";
            }
        }
        catch (Exception e)
        {
            response.Message = e.Message;
            _logger.LogError(e.Message);
        }

        return response;
    }

    public async Task<Response<int>> CountAsync()
    {
        var response = new Response<int>();

        try
        {
            response.Data = await _unitOfWork.Personas.CountAsync();

            if (response.Data > 0)
            {
                response.isSuccess = true;
                response.Message = "Personas encontradas";
            }
            else
            {
                response.Message = "No se encontraron personas";
            }
        }
        catch (Exception e)
        {
            response.Message = e.Message;
            _logger.LogError(e.Message);
        }

        return response;
    }

    public async Task<Response<PersonaDTO>> AuthenticateAsync(PersonaLoginDTO personaLogin)
    {
        var response = new Response<PersonaDTO>();

        try
        {
            var validation = _loginValidationRules.Validate(personaLogin);

            if (!validation.IsValid)
            {
                response.Message = validation.ToString();
                return response;
            }

            var persona = await _unitOfWork.Personas.AuthenticateAsync(personaLogin);

            if (persona != null)
            {
                response.Data = _mapper.Map<PersonaDTO>(persona);
                response.isSuccess = true;
                response.Message = "Persona autenticada";
            }
            else
            {
                response.Message = "Credenciales incorrectas";
            }
        }
        catch (Exception e)
        {
            response.Message = e.Message;
            _logger.LogError(e.Message);
        }

        return response;
    }

    #endregion
}