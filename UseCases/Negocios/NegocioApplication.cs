using Domain.Entities;
using DTO.Negocio;
using ErrorOr;
using Interface.Persistence;
using Interface.UseCases;
using Common;
using UseCases.Common.Mapping;
using Validator;

namespace UseCases.Negocios;

public class NegocioApplication : INegocioApplication
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IAppMapper _mapper;
    private readonly NegocioDTOValidator _validationRules;
    private readonly IAppLogger<NegocioApplication> _logger;

    public NegocioApplication(
        IUnitOfWork unitOfWork,
        IAppMapper mapper,
        NegocioDTOValidator validationRules,
        IAppLogger<NegocioApplication> logger)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _validationRules = validationRules;
        _logger = logger;
    }

    public async Task<ErrorOr<bool>> InsertAsync(NegocioDTO dto, CancellationToken cancellationToken = default)
    {
        var validation = await _validationRules.ValidateAsync(dto, cancellationToken);
        if (!validation.IsValid)
        {
            var firstError = validation.Errors[0];
            return Error.Validation(firstError.PropertyName, firstError.ErrorMessage);
        }

        var entity = _mapper.Map<Negocio>(dto);
        var success = await _unitOfWork.Negocios.InsertAsync(entity);
        return success ? true : Error.Failure("Negocio.InsertFailed", "No se pudo crear el registro.");
    }

    public async Task<ErrorOr<bool>> UpdateAsync(NegocioDTO dto, CancellationToken cancellationToken = default)
    {
        var validation = await _validationRules.ValidateAsync(dto, cancellationToken);
        if (!validation.IsValid)
        {
            var firstError = validation.Errors[0];
            return Error.Validation(firstError.PropertyName, firstError.ErrorMessage);
        }

        var entity = _mapper.Map<Negocio>(dto);
        var success = await _unitOfWork.Negocios.UpdateAsync(entity);
        return success ? true : Error.NotFound("Negocio.NotFound", "El registro especificado no existe o no pudo actualizarse.");
    }

    public async Task<ErrorOr<bool>> DeleteAsync(int id, CancellationToken cancellationToken = default)
    {
        var success = await _unitOfWork.Negocios.DeleteAsync(id);
        return success ? true : Error.NotFound("Negocio.NotFound", "No se encontro el registro a eliminar.");
    }

    public async Task<ErrorOr<NegocioDTO>> GetAsync(int id, CancellationToken cancellationToken = default)
    {
        var entity = await _unitOfWork.Negocios.GetAsync(id);
        if (entity is null)
        {
            return Error.NotFound("Negocio.NotFound", "El registro no fue encontrado.");
        }

        return _mapper.Map<NegocioDTO>(entity);
    }

    public async Task<ErrorOr<IEnumerable<NegocioDTO>>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        var entities = await _unitOfWork.Negocios.GetAllAsync();
        var dtos = _mapper.Map<IEnumerable<NegocioDTO>>(entities);
        return ErrorOrFactory.From(dtos);
    }

    public async Task<ErrorOr<IEnumerable<NegocioDTO>>> GetAllWithPaginationAsync(int page, int pageSize, CancellationToken cancellationToken = default)
    {
        var entities = await _unitOfWork.Negocios.GetAllWithPaginationAsync(page, pageSize);
        var dtos = _mapper.Map<IEnumerable<NegocioDTO>>(entities);
        return ErrorOrFactory.From(dtos);
    }

    public async Task<ErrorOr<int>> CountAsync(CancellationToken cancellationToken = default)
    {
        var count = await _unitOfWork.Negocios.CountAsync();
        return count;
    }
}
