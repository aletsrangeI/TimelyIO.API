using Domain.Entities;
using DTO.Cliente;
using ErrorOr;
using Interface.Persistence;
using Interface.UseCases;
using Common;
using UseCases.Common.Mapping;
using Validator;

namespace UseCases.Clientes;

public class ClienteApplication : IClienteApplication
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IAppMapper _mapper;
    private readonly ClienteDTOValidator _validationRules;
    private readonly IAppLogger<ClienteApplication> _logger;

    public ClienteApplication(
        IUnitOfWork unitOfWork,
        IAppMapper mapper,
        ClienteDTOValidator validationRules,
        IAppLogger<ClienteApplication> logger)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _validationRules = validationRules;
        _logger = logger;
    }

    public async Task<ErrorOr<bool>> InsertAsync(ClienteDTO dto, CancellationToken cancellationToken = default)
    {
        var validation = await _validationRules.ValidateAsync(dto, cancellationToken);
        if (!validation.IsValid)
        {
            var firstError = validation.Errors[0];
            return Error.Validation(firstError.PropertyName, firstError.ErrorMessage);
        }

        var entity = _mapper.Map<Cliente>(dto);
        var success = await _unitOfWork.Clientes.InsertAsync(entity);
        return success ? true : Error.Failure("Cliente.InsertFailed", "No se pudo crear el registro.");
    }

    public async Task<ErrorOr<bool>> UpdateAsync(ClienteDTO dto, CancellationToken cancellationToken = default)
    {
        var validation = await _validationRules.ValidateAsync(dto, cancellationToken);
        if (!validation.IsValid)
        {
            var firstError = validation.Errors[0];
            return Error.Validation(firstError.PropertyName, firstError.ErrorMessage);
        }

        var entity = _mapper.Map<Cliente>(dto);
        var success = await _unitOfWork.Clientes.UpdateAsync(entity);
        return success ? true : Error.NotFound("Cliente.NotFound", "El registro especificado no existe o no pudo actualizarse.");
    }

    public async Task<ErrorOr<bool>> DeleteAsync(int id, CancellationToken cancellationToken = default)
    {
        var success = await _unitOfWork.Clientes.DeleteAsync(id);
        return success ? true : Error.NotFound("Cliente.NotFound", "No se encontro el registro a eliminar.");
    }

    public async Task<ErrorOr<ClienteDTO>> GetAsync(int id, CancellationToken cancellationToken = default)
    {
        var entity = await _unitOfWork.Clientes.GetAsync(id);
        if (entity is null)
        {
            return Error.NotFound("Cliente.NotFound", "El registro no fue encontrado.");
        }

        return _mapper.Map<ClienteDTO>(entity);
    }

    public async Task<ErrorOr<IEnumerable<ClienteDTO>>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        var entities = await _unitOfWork.Clientes.GetAllAsync();
        var dtos = _mapper.Map<IEnumerable<ClienteDTO>>(entities);
        return ErrorOrFactory.From(dtos);
    }

    public async Task<ErrorOr<IEnumerable<ClienteDTO>>> GetAllWithPaginationAsync(int page, int pageSize, CancellationToken cancellationToken = default)
    {
        var entities = await _unitOfWork.Clientes.GetAllWithPaginationAsync(page, pageSize);
        var dtos = _mapper.Map<IEnumerable<ClienteDTO>>(entities);
        return ErrorOrFactory.From(dtos);
    }

    public async Task<ErrorOr<int>> CountAsync(CancellationToken cancellationToken = default)
    {
        var count = await _unitOfWork.Clientes.CountAsync();
        return count;
    }
}
