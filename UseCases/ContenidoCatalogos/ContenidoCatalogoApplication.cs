using AutoMapper;
using Common;
using Domain.Entities;
using DTO.ContenidoCatalogo;
using Interface.Persistence;
using Interface.UseCases;
using Validator;

namespace UseCases.ContenidoCatalogos;

public class ContenidoCatalogoApplication : IContenidoCatalogoApplication
{
    private readonly IAppLogger<ContenidoCatalogoApplication> _logger;
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ContenidoCatalogoDTOValidator _validationRules;

    public ContenidoCatalogoApplication(IAppLogger<ContenidoCatalogoApplication> logger, IMapper mapper,
        IUnitOfWork unitOfWork, ContenidoCatalogoDTOValidator validationRules)
    {
        _logger = logger;
        _mapper = mapper;
        _unitOfWork = unitOfWork;
        _validationRules = validationRules;
    }

    #region Metodos sincronos

    public Response<bool> Insert(ContenidoCatalogoDTO contenidoCatalogoDto)
    {
        var response = new Response<bool>();

        try
        {
            var contenidoCatalogo = _mapper.Map<ContenidoCatalogo>(contenidoCatalogoDto);
            response.Data = _unitOfWork.ContenidoCatalogos.Insert(contenidoCatalogo);

            if (response.Data)
            {
                response.isSuccess = true;
                response.Message = "ContenidoCatalogo insertado correctamente";
            }
        }
        catch (Exception e)
        {
            _logger.LogError(e.Message);
            response.Message = "Error al insertar el ContenidoCatalogo";
        }

        return response;
    }

    public Response<bool> Update(ContenidoCatalogoDTO contenidoCatalogoDto)
    {
        var response = new Response<bool>();

        try
        {
            var contenidoCatalogo = _mapper.Map<ContenidoCatalogo>(contenidoCatalogoDto);
            response.Data = _unitOfWork.ContenidoCatalogos.Update(contenidoCatalogo);

            if (response.Data)
            {
                response.isSuccess = true;
                response.Message = "ContenidoCatalogo actualizado correctamente";
            }
        }
        catch (Exception e)
        {
            _logger.LogError(e.Message);
            response.Message = "Error al actualizar el ContenidoCatalogo";
        }

        return response;
    }

    public Response<bool> Delete(int id)
    {
        var response = new Response<bool>();

        try
        {
            response.Data = _unitOfWork.ContenidoCatalogos.Delete(id);

            if (response.Data)
            {
                response.isSuccess = true;
                response.Message = "ContenidoCatalogo eliminado correctamente";
            }
        }
        catch (Exception e)
        {
            _logger.LogError(e.Message);
            response.Message = "Error al eliminar el ContenidoCatalogo";
        }

        return response;
    }

    public Response<ContenidoCatalogoDTO> Get(int id)
    {
        var response = new Response<ContenidoCatalogoDTO>();

        try
        {
            var contenidoCatalogo = _unitOfWork.ContenidoCatalogos.Get(id);
            response.Data = _mapper.Map<ContenidoCatalogoDTO>(contenidoCatalogo);

            if (response.Data != null)
            {
                response.isSuccess = true;
                response.Message = "ContenidoCatalogo obtenido correctamente";
            }
        }
        catch (Exception e)
        {
            _logger.LogError(e.Message);
            response.Message = "Error al obtener el ContenidoCatalogo";
        }

        return response;
    }

    public Response<IEnumerable<ContenidoCatalogoDTO>> getAll()
    {
        var response = new Response<IEnumerable<ContenidoCatalogoDTO>>();

        try
        {
            var contenidoCatalogos = _unitOfWork.ContenidoCatalogos.GetAll();
            response.Data = _mapper.Map<IEnumerable<ContenidoCatalogoDTO>>(contenidoCatalogos);

            if (response.Data != null)
            {
                response.isSuccess = true;
                response.Message = "ContenidoCatalogos obtenidos correctamente";
            }
        }
        catch (Exception e)
        {
            _logger.LogError(e.Message);
            response.Message = "Error al obtener los ContenidoCatalogos";
        }

        return response;
    }

    public Response<IEnumerable<ContenidoCatalogoDTO>> GetAll()
    {
        var response = new Response<IEnumerable<ContenidoCatalogoDTO>>();

        try
        {
            var contenidoCatalogos = _unitOfWork.ContenidoCatalogos.GetAll();
            response.Data = _mapper.Map<IEnumerable<ContenidoCatalogoDTO>>(contenidoCatalogos);

            if (response.Data != null)
            {
                response.isSuccess = true;
                response.Message = "ContenidoCatalogos obtenidos correctamente";
            }
        }
        catch (Exception e)
        {
            _logger.LogError(e.Message);
            response.Message = "Error al obtener los ContenidoCatalogos";
        }

        return response;
    }

    public ResponsePagination<IEnumerable<ContenidoCatalogoDTO>> GetAllWithPagination(int page, int pageSize)
    {
        var response = new ResponsePagination<IEnumerable<ContenidoCatalogoDTO>>();

        try
        {
            var contenidoCatalogos = _unitOfWork.ContenidoCatalogos.GetAllWithPagination(page, pageSize);
            response.Data = _mapper.Map<IEnumerable<ContenidoCatalogoDTO>>(contenidoCatalogos);

            if (response.Data != null)
            {
                response.isSuccess = true;
                response.Message = "ContenidoCatalogos obtenidos correctamente";
            }
        }
        catch (Exception e)
        {
            _logger.LogError(e.Message);
            response.Message = "Error al obtener los ContenidoCatalogos";
        }

        return response;
    }

    public Response<int> Count()
    {
        var response = new Response<int>();

        try
        {
            response.Data = _unitOfWork.ContenidoCatalogos.Count();

            if (response.Data > 0)
            {
                response.isSuccess = true;
                response.Message = "ContenidoCatalogos contados correctamente";
            }
        }
        catch (Exception e)
        {
            _logger.LogError(e.Message);
            response.Message = "Error al contar los ContenidoCatalogos";
        }

        return response;
    }

    public Response<IEnumerable<ContenidoCatalogoDTO>> GetContenidoCatalogoByCatalogoId(int id)
    {
        var response = new Response<IEnumerable<ContenidoCatalogoDTO>>();

        try
        {
            var contenidoCatalogos = _unitOfWork.ContenidoCatalogos.GetContenidoCatalogoByCatalogoId(id);
            response.Data = _mapper.Map<IEnumerable<ContenidoCatalogoDTO>>(contenidoCatalogos);

            if (response.Data != null)
            {
                response.isSuccess = true;
                response.Message = "ContenidoCatalogos obtenidos correctamente";
            }
        }
        catch (Exception e)
        {
            _logger.LogError(e.Message);
            response.Message = "Error al obtener los ContenidoCatalogos";
        }

        return response;
    }

    #endregion

    #region Metodos asincronos

    public async Task<Response<bool>> InsertAsync(ContenidoCatalogoDTO contenidoCatalogoDto)
    {
        var response = new Response<bool>();

        try
        {
            var contenidoCatalogo = _mapper.Map<ContenidoCatalogo>(contenidoCatalogoDto);
            response.Data = await _unitOfWork.ContenidoCatalogos.InsertAsync(contenidoCatalogo);

            if (response.Data)
            {
                response.isSuccess = true;
                response.Message = "ContenidoCatalogo insertado correctamente";
            }
        }
        catch (Exception e)
        {
            _logger.LogError(e.Message);
            response.Message = "Error al insertar el ContenidoCatalogo";
        }

        return response;
    }

    public async Task<Response<bool>> UpdateAsync(ContenidoCatalogoDTO contenidoCatalogoDto)
    {
        var response = new Response<bool>();

        try
        {
            var contenidoCatalogo = _mapper.Map<ContenidoCatalogo>(contenidoCatalogoDto);
            response.Data = await _unitOfWork.ContenidoCatalogos.UpdateAsync(contenidoCatalogo);

            if (response.Data)
            {
                response.isSuccess = true;
                response.Message = "ContenidoCatalogo actualizado correctamente";
            }
        }
        catch (Exception e)
        {
            _logger.LogError(e.Message);
            response.Message = "Error al actualizar el ContenidoCatalogo";
        }

        return response;
    }

    public async Task<Response<bool>> DeleteAsync(int id)
    {
        var response = new Response<bool>();

        try
        {
            response.Data = await _unitOfWork.ContenidoCatalogos.DeleteAsync(id);

            if (response.Data)
            {
                response.isSuccess = true;
                response.Message = "ContenidoCatalogo eliminado correctamente";
            }
        }
        catch (Exception e)
        {
            _logger.LogError(e.Message);
            response.Message = "Error al eliminar el ContenidoCatalogo";
        }

        return response;
    }

    public async Task<Response<ContenidoCatalogoDTO>> GetAsync(int id)
    {
        var response = new Response<ContenidoCatalogoDTO>();

        try
        {
            var contenidoCatalogo = await _unitOfWork.ContenidoCatalogos.GetAsync(id);
            response.Data = _mapper.Map<ContenidoCatalogoDTO>(contenidoCatalogo);

            if (response.Data != null)
            {
                response.isSuccess = true;
                response.Message = "ContenidoCatalogo obtenido correctamente";
            }
        }
        catch (Exception e)
        {
            _logger.LogError(e.Message);
            response.Message = "Error al obtener el ContenidoCatalogo";
        }

        return response;
    }

    public async Task<Response<IEnumerable<ContenidoCatalogoDTO>>> GetAllAsync()
    {
        var response = new Response<IEnumerable<ContenidoCatalogoDTO>>();

        try
        {
            var contenidoCatalogos = await _unitOfWork.ContenidoCatalogos.GetAllAsync();
            response.Data = _mapper.Map<IEnumerable<ContenidoCatalogoDTO>>(contenidoCatalogos);

            if (response.Data != null)
            {
                response.isSuccess = true;
                response.Message = "ContenidoCatalogos obtenidos correctamente";
            }
        }
        catch (Exception e)
        {
            _logger.LogError(e.Message);
            response.Message = "Error al obtener los ContenidoCatalogos";
        }

        return response;
    }

    public async Task<ResponsePagination<IEnumerable<ContenidoCatalogoDTO>>> GetAllWithPaginationAsync(int page,
        int pageSize)
    {
        var response = new ResponsePagination<IEnumerable<ContenidoCatalogoDTO>>();

        try
        {
            var contenidoCatalogos = await _unitOfWork.ContenidoCatalogos.GetAllWithPaginationAsync(page, pageSize);
            response.Data = _mapper.Map<IEnumerable<ContenidoCatalogoDTO>>(contenidoCatalogos);

            if (response.Data != null)
            {
                response.isSuccess = true;
                response.Message = "ContenidoCatalogos obtenidos correctamente";
            }
        }
        catch (Exception e)
        {
            _logger.LogError(e.Message);
            response.Message = "Error al obtener los ContenidoCatalogos";
        }

        return response;
    }

    public async Task<Response<int>> CountAsync()
    {
        var response = new Response<int>();

        try
        {
            response.Data = await _unitOfWork.ContenidoCatalogos.CountAsync();

            if (response.Data > 0)
            {
                response.isSuccess = true;
                response.Message = "ContenidoCatalogos contados correctamente";
            }
        }
        catch (Exception e)
        {
            _logger.LogError(e.Message);
            response.Message = "Error al contar los ContenidoCatalogos";
        }

        return response;
    }

    public async Task<Response<IEnumerable<ContenidoCatalogoDTO>>> GetContenidoCatalogoByCatalogoIdAsync(int id)
    {
        var response = new Response<IEnumerable<ContenidoCatalogoDTO>>();

        try
        {
            var contenidoCatalogos = await _unitOfWork.ContenidoCatalogos.GetContenidoCatalogoByCatalogoIdAsync(id);
            response.Data = _mapper.Map<IEnumerable<ContenidoCatalogoDTO>>(contenidoCatalogos);

            if (response.Data != null)
            {
                response.isSuccess = true;
                response.Message = "ContenidoCatalogos obtenidos correctamente";
            }
        }
        catch (Exception e)
        {
            _logger.LogError(e.Message);
            response.Message = "Error al obtener los ContenidoCatalogos";
        }

        return response;
    }

    #endregion
}