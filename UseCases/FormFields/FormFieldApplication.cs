using AutoMapper;
using Common;
using Domain.Entities;
using DTO.FormField;
using Interface.Persistence;
using Interface.UseCases;
using Validator;

namespace UseCases.FormFields;

public class FormFieldApplication : IFormFieldApplication
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly FormFieldDTOValidator _validationRules;
    private readonly IAppLogger<FormFieldApplication> _logger;

    public FormFieldApplication(IUnitOfWork unitOfWork, IMapper mapper, FormFieldDTOValidator validationRules,
        IAppLogger<FormFieldApplication> logger)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _validationRules = validationRules;
        _logger = logger;
    }

    #region Metodos sincronos

    public Response<bool> Insert(FormFieldDTO formFieldDto)
    {
        var response = new Response<bool>();

        try
        {
            var formField = _mapper.Map<FormField>(formFieldDto);
            response.Data = _unitOfWork.FormFields.Insert(formField);

            if (response.Data)
            {
                response.isSuccess = true;
                response.Message = "Se ha insertado correctamente el campo del formulario";
            }
        }
        catch (Exception e)
        {
            response.Message = e.Message;
            _logger.LogError(e.Message);
        }

        return response;
    }

    public Response<bool> Update(FormFieldDTO formFieldDto)
    {
        var response = new Response<bool>();

        try
        {
            var formField = _mapper.Map<FormField>(formFieldDto);
            response.Data = _unitOfWork.FormFields.Update(formField);

            if (response.Data)
            {
                response.isSuccess = true;
                response.Message = "Se ha actualizado correctamente el campo del formulario";
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
            response.Data = _unitOfWork.FormFields.Delete(id);

            if (response.Data)
            {
                response.isSuccess = true;
                response.Message = "Se ha eliminado correctamente el campo del formulario";
            }
        }
        catch (Exception e)
        {
            response.Message = e.Message;
            _logger.LogError(e.Message);
        }

        return response;
    }

    public Response<FormFieldDTO> Get(int id)
    {
        var response = new Response<FormFieldDTO>();

        try
        {
            var formField = _unitOfWork.FormFields.Get(id);
            response.Data = _mapper.Map<FormFieldDTO>(formField);

            if (response.Data != null)
            {
                response.isSuccess = true;
                response.Message = "Se ha obtenido correctamente el campo del formulario";
            }
        }
        catch (Exception e)
        {
            response.Message = e.Message;
            _logger.LogError(e.Message);
        }

        return response;
    }

    public Response<IEnumerable<FormFieldDTO>> GetAll()
    {
        var response = new Response<IEnumerable<FormFieldDTO>>();

        try
        {
            var formFields = _unitOfWork.FormFields.GetAll();
            response.Data = _mapper.Map<IEnumerable<FormFieldDTO>>(formFields);

            if (response.Data != null)
            {
                response.isSuccess = true;
                response.Message = "Se han obtenido correctamente los campos del formulario";
            }
        }
        catch (Exception e)
        {
            response.Message = e.Message;
            _logger.LogError(e.Message);
        }

        return response;
    }

    public ResponsePagination<IEnumerable<FormFieldDTO>> GetAllWithPagination(int page, int pageSize)
    {
        var response = new ResponsePagination<IEnumerable<FormFieldDTO>>();

        try
        {
            var formFields = _unitOfWork.FormFields.GetAllWithPagination(page, pageSize);
            response.Data = _mapper.Map<IEnumerable<FormFieldDTO>>(formFields);

            if (response.Data != null)
            {
                response.isSuccess = true;
                response.Message = "Se han obtenido correctamente los campos del formulario";
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
            response.Data = _unitOfWork.FormFields.Count();

            if (response.Data > 0)
            {
                response.isSuccess = true;
                response.Message = "Se ha obtenido correctamente el total de campos del formulario";
            }
        }
        catch (Exception e)
        {
            response.Message = e.Message;
            _logger.LogError(e.Message);
        }

        return response;
    }

    public Response<IEnumerable<FormFieldDTO>> GetFormFieldByFormCatId(int id)
    {
        var response = new Response<IEnumerable<FormFieldDTO>>();

        try
        {
            var formFields = _unitOfWork.FormFields.GetFormFieldByFormCatId(id);
            response.Data = _mapper.Map<IEnumerable<FormFieldDTO>>(formFields);

            if (response.Data != null)
            {
                response.isSuccess = true;
                response.Message = "Se han obtenido correctamente los campos del formulario";
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

    public async Task<Response<bool>> InsertAsync(FormFieldDTO formFieldDto)
    {
        var response = new Response<bool>();

        try
        {
            var formField = _mapper.Map<FormField>(formFieldDto);
            response.Data = await _unitOfWork.FormFields.InsertAsync(formField);

            if (response.Data)
            {
                response.isSuccess = true;
                response.Message = "Se ha insertado correctamente el campo del formulario";
            }
        }
        catch (Exception e)
        {
            response.Message = e.Message;
            _logger.LogError(e.Message);
        }

        return response;
    }

    public async Task<Response<bool>> UpdateAsync(FormFieldDTO formFieldDto)
    {
        var response = new Response<bool>();

        try
        {
            var formField = _mapper.Map<FormField>(formFieldDto);
            response.Data = await _unitOfWork.FormFields.UpdateAsync(formField);

            if (response.Data)
            {
                response.isSuccess = true;
                response.Message = "Se ha actualizado correctamente el campo del formulario";
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
            response.Data = await _unitOfWork.FormFields.DeleteAsync(id);

            if (response.Data)
            {
                response.isSuccess = true;
                response.Message = "Se ha eliminado correctamente el campo del formulario";
            }
        }
        catch (Exception e)
        {
            response.Message = e.Message;
            _logger.LogError(e.Message);
        }

        return response;
    }

    public async Task<Response<FormFieldDTO>> GetAsync(int id)
    {
        var response = new Response<FormFieldDTO>();

        try
        {
            var formField = await _unitOfWork.FormFields.GetAsync(id);
            response.Data = _mapper.Map<FormFieldDTO>(formField);

            if (response.Data != null)
            {
                response.isSuccess = true;
                response.Message = "Se ha obtenido correctamente el campo del formulario";
            }
        }
        catch (Exception e)
        {
            response.Message = e.Message;
            _logger.LogError(e.Message);
        }

        return response;
    }

    public async Task<Response<IEnumerable<FormFieldDTO>>> GetAllAsync()
    {
        var response = new Response<IEnumerable<FormFieldDTO>>();

        try
        {
            var formFields = await _unitOfWork.FormFields.GetAllAsync();
            response.Data = _mapper.Map<IEnumerable<FormFieldDTO>>(formFields);

            if (response.Data != null)
            {
                response.isSuccess = true;
                response.Message = "Se han obtenido correctamente los campos del formulario";
            }
        }
        catch (Exception e)
        {
            response.Message = e.Message;
            _logger.LogError(e.Message);
        }

        return response;
    }

    public async Task<ResponsePagination<IEnumerable<FormFieldDTO>>> GetAllWithPaginationAsync(int page, int pageSize)
    {
        var response = new ResponsePagination<IEnumerable<FormFieldDTO>>();

        try
        {
            var formFields = await _unitOfWork.FormFields.GetAllWithPaginationAsync(page, pageSize);
            response.Data = _mapper.Map<IEnumerable<FormFieldDTO>>(formFields);

            if (response.Data != null)
            {
                response.isSuccess = true;
                response.Message = "Se han obtenido correctamente los campos del formulario";
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
            response.Data = await _unitOfWork.FormFields.CountAsync();

            if (response.Data > 0)
            {
                response.isSuccess = true;
                response.Message = "Se ha obtenido correctamente el total de campos del formulario";
            }
        }
        catch (Exception e)
        {
            response.Message = e.Message;
            _logger.LogError(e.Message);
        }

        return response;
    }

    public async Task<Response<IEnumerable<FormFieldDTO>>> GetFormFieldByFormCatIdAsync(int id)
    {
        var response = new Response<IEnumerable<FormFieldDTO>>();

        try
        {
            var formFields = await _unitOfWork.FormFields.GetFormFieldByFormCatIdAsync(id);
            response.Data = _mapper.Map<IEnumerable<FormFieldDTO>>(formFields);

            if (response.Data != null)
            {
                response.isSuccess = true;
                response.Message = "Se han obtenido correctamente los campos del formulario";
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