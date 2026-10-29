using Domain.Entities;
using DTO.Servicio;
using ErrorOr;
using Interface.Persistence;
using Interface.UseCases;
using Common;
using UseCases.Common.Mapping;
using Validator;

namespace UseCases.Servicios;

public class ServicioApplication : IServicioApplication
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IAppMapper _mapper;
    private readonly ServicioDTOValidator _validationRules;
    private readonly IAppLogger<ServicioApplication> _logger;

    public ServicioApplication(
        IUnitOfWork unitOfWork,
        IAppMapper mapper,
        ServicioDTOValidator validationRules,
        IAppLogger<ServicioApplication> logger)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _validationRules = validationRules;
        _logger = logger;
    }

    public async Task<ErrorOr<bool>> InsertAsync(ServicioDTO dto, CancellationToken cancellationToken = default)
    {
        var validation = await _validationRules.ValidateAsync(dto, cancellationToken);
        if (!validation.IsValid)
        {
            var firstError = validation.Errors[0];
            return Error.Validation(firstError.PropertyName, firstError.ErrorMessage);
        }

        var entity = _mapper.Map<Servicio>(dto);
        var success = await _unitOfWork.Servicios.InsertAsync(entity);
        return success ? true : Error.Failure("Servicio.InsertFailed", "No se pudo crear el registro.");
    }

    public async Task<ErrorOr<bool>> UpdateAsync(ServicioDTO dto, CancellationToken cancellationToken = default)
    {
        var validation = await _validationRules.ValidateAsync(dto, cancellationToken);
        if (!validation.IsValid)
        {
            var firstError = validation.Errors[0];
            return Error.Validation(firstError.PropertyName, firstError.ErrorMessage);
        }

        var entity = _mapper.Map<Servicio>(dto);
        var success = await _unitOfWork.Servicios.UpdateAsync(entity);
        return success ? true : Error.NotFound("Servicio.NotFound", "El registro especificado no existe o no pudo actualizarse.");
    }

    public async Task<ErrorOr<bool>> DeleteAsync(int id, CancellationToken cancellationToken = default)
    {
        var success = await _unitOfWork.Servicios.DeleteAsync(id);
        return success ? true : Error.NotFound("Servicio.NotFound", "No se encontro el registro a eliminar.");
    }

    public async Task<ErrorOr<ServicioDTO>> GetAsync(int id, CancellationToken cancellationToken = default)
    {
        var entity = await _unitOfWork.Servicios.GetAsync(id);
        if (entity is null)
        {
            return Error.NotFound("Servicio.NotFound", "El registro no fue encontrado.");
        }

        return _mapper.Map<ServicioDTO>(entity);
    }

    public async Task<ErrorOr<IEnumerable<ServicioDTO>>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        var entities = await _unitOfWork.Servicios.GetAllAsync();
        var dtos = _mapper.Map<IEnumerable<ServicioDTO>>(entities);
        return ErrorOrFactory.From(dtos);
    }

    public async Task<ErrorOr<IEnumerable<ServicioDTO>>> GetAllWithPaginationAsync(int page, int pageSize, CancellationToken cancellationToken = default)
    {
        var entities = await _unitOfWork.Servicios.GetAllWithPaginationAsync(page, pageSize);
        var dtos = _mapper.Map<IEnumerable<ServicioDTO>>(entities);
        return ErrorOrFactory.From(dtos);
    }

    public async Task<ErrorOr<int>> CountAsync(CancellationToken cancellationToken = default)
    {
        var count = await _unitOfWork.Servicios.CountAsync();
        return count;
    }
}
