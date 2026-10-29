using Domain.Entities;
using DTO.Profesional;
using ErrorOr;
using Interface.Persistence;
using Interface.UseCases;
using Common;
using UseCases.Common.Mapping;
using Validator;

namespace UseCases.Profesionales;

public class ProfesionalApplication : IProfesionalApplication
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IAppMapper _mapper;
    private readonly ProfesionalDTOValidator _validationRules;
    private readonly IAppLogger<ProfesionalApplication> _logger;

    public ProfesionalApplication(
        IUnitOfWork unitOfWork,
        IAppMapper mapper,
        ProfesionalDTOValidator validationRules,
        IAppLogger<ProfesionalApplication> logger)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _validationRules = validationRules;
        _logger = logger;
    }

    public async Task<ErrorOr<bool>> InsertAsync(ProfesionalDTO dto, CancellationToken cancellationToken = default)
    {
        var validation = await _validationRules.ValidateAsync(dto, cancellationToken);
        if (!validation.IsValid)
        {
            var firstError = validation.Errors[0];
            return Error.Validation(firstError.PropertyName, firstError.ErrorMessage);
        }

        var entity = _mapper.Map<Profesional>(dto);
        var success = await _unitOfWork.Profesionales.InsertAsync(entity);
        return success ? true : Error.Failure("Profesional.InsertFailed", "No se pudo crear el registro.");
    }

    public async Task<ErrorOr<bool>> UpdateAsync(ProfesionalDTO dto, CancellationToken cancellationToken = default)
    {
        var validation = await _validationRules.ValidateAsync(dto, cancellationToken);
        if (!validation.IsValid)
        {
            var firstError = validation.Errors[0];
            return Error.Validation(firstError.PropertyName, firstError.ErrorMessage);
        }

        var entity = _mapper.Map<Profesional>(dto);
        var success = await _unitOfWork.Profesionales.UpdateAsync(entity);
        return success ? true : Error.NotFound("Profesional.NotFound", "El registro especificado no existe o no pudo actualizarse.");
    }

    public async Task<ErrorOr<bool>> DeleteAsync(int id, CancellationToken cancellationToken = default)
    {
        var success = await _unitOfWork.Profesionales.DeleteAsync(id);
        return success ? true : Error.NotFound("Profesional.NotFound", "No se encontro el registro a eliminar.");
    }

    public async Task<ErrorOr<ProfesionalDTO>> GetAsync(int id, CancellationToken cancellationToken = default)
    {
        var entity = await _unitOfWork.Profesionales.GetAsync(id);
        if (entity is null)
        {
            return Error.NotFound("Profesional.NotFound", "El registro no fue encontrado.");
        }

        return _mapper.Map<ProfesionalDTO>(entity);
    }

    public async Task<ErrorOr<IEnumerable<ProfesionalDTO>>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        var entities = await _unitOfWork.Profesionales.GetAllAsync();
        var dtos = _mapper.Map<IEnumerable<ProfesionalDTO>>(entities);
        return ErrorOrFactory.From(dtos);
    }

    public async Task<ErrorOr<IEnumerable<ProfesionalDTO>>> GetAllWithPaginationAsync(int page, int pageSize, CancellationToken cancellationToken = default)
    {
        var entities = await _unitOfWork.Profesionales.GetAllWithPaginationAsync(page, pageSize);
        var dtos = _mapper.Map<IEnumerable<ProfesionalDTO>>(entities);
        return ErrorOrFactory.From(dtos);
    }

    public async Task<ErrorOr<int>> CountAsync(CancellationToken cancellationToken = default)
    {
        var count = await _unitOfWork.Profesionales.CountAsync();
        return count;
    }
}
