using Domain.Entities;
using DTO.ExpedienteCliente;
using ErrorOr;
using Interface.Persistence;
using Interface.UseCases;
using Common;
using UseCases.Common.Mapping;
using Validator;

namespace UseCases.ExpedientesClientes;

public class ExpedienteClienteApplication : IExpedienteClienteApplication
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IAppMapper _mapper;
    private readonly ExpedienteClienteDTOValidator _validationRules;
    private readonly IAppLogger<ExpedienteClienteApplication> _logger;

    public ExpedienteClienteApplication(
        IUnitOfWork unitOfWork,
        IAppMapper mapper,
        ExpedienteClienteDTOValidator validationRules,
        IAppLogger<ExpedienteClienteApplication> logger)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _validationRules = validationRules;
        _logger = logger;
    }

    public async Task<ErrorOr<bool>> InsertAsync(ExpedienteClienteDTO dto, CancellationToken cancellationToken = default)
    {
        var validation = await _validationRules.ValidateAsync(dto, cancellationToken);
        if (!validation.IsValid)
        {
            var firstError = validation.Errors[0];
            return Error.Validation(firstError.PropertyName, firstError.ErrorMessage);
        }

        var entity = _mapper.Map<ExpedienteCliente>(dto);
        var success = await _unitOfWork.ExpedientesClientes.InsertAsync(entity);
        return success ? true : Error.Failure("ExpedienteCliente.InsertFailed", "No se pudo crear el registro.");
    }

    public async Task<ErrorOr<bool>> UpdateAsync(ExpedienteClienteDTO dto, CancellationToken cancellationToken = default)
    {
        var validation = await _validationRules.ValidateAsync(dto, cancellationToken);
        if (!validation.IsValid)
        {
            var firstError = validation.Errors[0];
            return Error.Validation(firstError.PropertyName, firstError.ErrorMessage);
        }

        var entity = _mapper.Map<ExpedienteCliente>(dto);
        var success = await _unitOfWork.ExpedientesClientes.UpdateAsync(entity);
        return success ? true : Error.NotFound("ExpedienteCliente.NotFound", "El registro especificado no existe o no pudo actualizarse.");
    }

    public async Task<ErrorOr<bool>> DeleteAsync(int id, CancellationToken cancellationToken = default)
    {
        var success = await _unitOfWork.ExpedientesClientes.DeleteAsync(id);
        return success ? true : Error.NotFound("ExpedienteCliente.NotFound", "No se encontro el registro a eliminar.");
    }

    public async Task<ErrorOr<ExpedienteClienteDTO>> GetAsync(int id, CancellationToken cancellationToken = default)
    {
        var entity = await _unitOfWork.ExpedientesClientes.GetAsync(id);
        if (entity is null)
        {
            return Error.NotFound("ExpedienteCliente.NotFound", "El registro no fue encontrado.");
        }

        return _mapper.Map<ExpedienteClienteDTO>(entity);
    }

    public async Task<ErrorOr<IEnumerable<ExpedienteClienteDTO>>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        var entities = await _unitOfWork.ExpedientesClientes.GetAllAsync();
        var dtos = _mapper.Map<IEnumerable<ExpedienteClienteDTO>>(entities);
        return ErrorOrFactory.From(dtos);
    }

    public async Task<ErrorOr<IEnumerable<ExpedienteClienteDTO>>> GetAllWithPaginationAsync(int page, int pageSize, CancellationToken cancellationToken = default)
    {
        var entities = await _unitOfWork.ExpedientesClientes.GetAllWithPaginationAsync(page, pageSize);
        var dtos = _mapper.Map<IEnumerable<ExpedienteClienteDTO>>(entities);
        return ErrorOrFactory.From(dtos);
    }

    public async Task<ErrorOr<int>> CountAsync(CancellationToken cancellationToken = default)
    {
        var count = await _unitOfWork.ExpedientesClientes.CountAsync();
        return count;
    }
}
