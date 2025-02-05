using AutoMapper;
using Common;
using Domain.Entities;
using DTO;
using Interface.Persistence;
using Interface.UseCases;
using Validator;

namespace UseCases.Catalogos;

public class CatalogoApplication : ICatalogoApplication
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly CatalogoDTOValidator _validationRules;
    private readonly IAppLogger<CatalogoApplication> _logger;

    public CatalogoApplication(IUnitOfWork unitOfWork, IMapper mapper, CatalogoDTOValidator validationRules,
        IAppLogger<CatalogoApplication> logger)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _validationRules = validationRules;
        _logger = logger;
    }

    #region Metodos sincronos

    public Response<bool> Insert(CatalogoDTO catalogoDto)
    {
        var response = new Response<bool>();

        try
        {
            var catalogo = _mapper.Map<Catalogo>(catalogoDto);
            response.Data = _unitOfWork.Catalogos.Insert(catalogo);

            if (response.Data)
            {
                response.isSuccess = true;
                response.Message = "Catalogo insertado correctamente";
            }
        }
        catch (Exception e)
        {
            response.Message = e.Message;
            _logger.LogError(e.Message);
        }

        return response;
    }

    public Response<bool> Update(CatalogoDTO catalogoDto)
    {
        var response = new Response<bool>();

        try
        {
            var catalogo = _mapper.Map<Catalogo>(catalogoDto);
            response.Data = _unitOfWork.Catalogos.Update(catalogo);

            if (response.Data)
            {
                response.isSuccess = true;
                response.Message = "Catalogo actualizado correctamente";
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
            response.Data = _unitOfWork.Catalogos.Delete(id);

            if (response.Data)
            {
                response.isSuccess = true;
                response.Message = "Catalogo eliminado correctamente";
            }
        }
        catch (Exception e)
        {
            response.Message = e.Message;
            _logger.LogError(e.Message);
        }

        return response;
    }

    public Response<CatalogoDTO> Get(int id)
    {
        var response = new Response<CatalogoDTO>();

        try
        {
            var catalogo = _unitOfWork.Catalogos.Get(id);
            response.Data = _mapper.Map<CatalogoDTO>(catalogo);

            if (response.Data != null)
            {
                response.isSuccess = true;
                response.Message = "Catalogo encontrado";
            }
        }
        catch (Exception e)
        {
            response.Message = e.Message;
            _logger.LogError(e.Message);
        }

        return response;
    }

    public Response<IEnumerable<CatalogoDTO>> GetAll()
    {
        var response = new Response<IEnumerable<CatalogoDTO>>();

        try
        {
            var catalogos = _unitOfWork.Catalogos.GetAll();
            response.Data = _mapper.Map<IEnumerable<CatalogoDTO>>(catalogos);

            if (response.Data != null)
            {
                response.isSuccess = true;
                response.Message = "Catalogos encontrados";
            }
        }
        catch (Exception e)
        {
            response.Message = e.Message;
            _logger.LogError(e.Message);
        }

        return response;
    }

    public ResponsePagination<IEnumerable<CatalogoDTO>> GetAllWithPagination(int page, int pageSize)
    {
        var response = new ResponsePagination<IEnumerable<CatalogoDTO>>();

        try
        {
            var catalogos = _unitOfWork.Catalogos.GetAllWithPagination(page, pageSize);
            response.Data = _mapper.Map<IEnumerable<CatalogoDTO>>(catalogos);

            if (response.Data != null)
            {
                response.isSuccess = true;
                response.Message = "Catalogos encontrados";
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
            response.Data = _unitOfWork.Catalogos.Count();

            if (response.Data > 0)
            {
                response.isSuccess = true;
                response.Message = "Catalogos encontrados";
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

    public async Task<Response<bool>> InsertAsync(CatalogoDTO catalogoDto)
    {
        var response = new Response<bool>();

        try
        {
            var catalogo = _mapper.Map<Catalogo>(catalogoDto);
            response.Data = await _unitOfWork.Catalogos.InsertAsync(catalogo);

            if (response.Data)
            {
                response.isSuccess = true;
                response.Message = "Catalogo insertado correctamente";
            }
        }
        catch (Exception e)
        {
            response.Message = e.Message;
            _logger.LogError(e.Message);
        }

        return response;
    }

    public async Task<Response<bool>> UpdateAsync(CatalogoDTO catalogoDto)
    {
        var response = new Response<bool>();

        try
        {
            var catalogo = _mapper.Map<Catalogo>(catalogoDto);
            response.Data = await _unitOfWork.Catalogos.UpdateAsync(catalogo);

            if (response.Data)
            {
                response.isSuccess = true;
                response.Message = "Catalogo actualizado correctamente";
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
            response.Data = await _unitOfWork.Catalogos.DeleteAsync(id);

            if (response.Data)
            {
                response.isSuccess = true;
                response.Message = "Catalogo eliminado correctamente";
            }
        }
        catch (Exception e)
        {
            response.Message = e.Message;
            _logger.LogError(e.Message);
        }

        return response;
    }

    public async Task<Response<CatalogoDTO>> GetAsync(int id)
    {
        var response = new Response<CatalogoDTO>();

        try
        {
            var catalogo = await _unitOfWork.Catalogos.GetAsync(id);
            response.Data = _mapper.Map<CatalogoDTO>(catalogo);

            if (response.Data != null)
            {
                response.isSuccess = true;
                response.Message = "Catalogo encontrado";
            }
        }
        catch (Exception e)
        {
            response.Message = e.Message;
            _logger.LogError(e.Message);
        }

        return response;
    }

    public async Task<Response<IEnumerable<CatalogoDTO>>> GetAllAsync()
    {
        var response = new Response<IEnumerable<CatalogoDTO>>();

        try
        {
            var catalogos = await _unitOfWork.Catalogos.GetAllAsync();
            response.Data = _mapper.Map<IEnumerable<CatalogoDTO>>(catalogos);

            if (response.Data != null)
            {
                response.isSuccess = true;
                response.Message = "Catalogos encontrados";
            }
        }
        catch (Exception e)
        {
            response.Message = e.Message;
            _logger.LogError(e.Message);
        }

        return response;
    }

    public async Task<ResponsePagination<IEnumerable<CatalogoDTO>>> GetAllWithPaginationAsync(int page, int pageSize)
    {
        var response = new ResponsePagination<IEnumerable<CatalogoDTO>>();

        try
        {
            var catalogos = await _unitOfWork.Catalogos.GetAllWithPaginationAsync(page, pageSize);
            response.Data = _mapper.Map<IEnumerable<CatalogoDTO>>(catalogos);

            if (response.Data != null)
            {
                response.isSuccess = true;
                response.Message = "Catalogos encontrados";
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
            response.Data = await _unitOfWork.Catalogos.CountAsync();

            if (response.Data > 0)
            {
                response.isSuccess = true;
                response.Message = "Catalogos encontrados";
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