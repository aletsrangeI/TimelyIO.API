using DTO.ExpedienteCliente;
using ErrorOr;

namespace Interface.UseCases;

/// <summary>
/// Contrato de aplicacion asincrono puro gobernado por el patron Result (ErrorOr).
/// Elimina el lanzamiento de excepciones para flujos de error semanticos.
/// </summary>
public interface IExpedienteClienteApplication
{
    Task<ErrorOr<bool>> InsertAsync(ExpedienteClienteDTO dto, CancellationToken cancellationToken = default);
    Task<ErrorOr<bool>> UpdateAsync(ExpedienteClienteDTO dto, CancellationToken cancellationToken = default);
    Task<ErrorOr<bool>> DeleteAsync(int id, CancellationToken cancellationToken = default);
    Task<ErrorOr<ExpedienteClienteDTO>> GetAsync(int id, CancellationToken cancellationToken = default);
    Task<ErrorOr<IEnumerable<ExpedienteClienteDTO>>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<ErrorOr<IEnumerable<ExpedienteClienteDTO>>> GetAllWithPaginationAsync(int page, int pageSize, CancellationToken cancellationToken = default);
    Task<ErrorOr<int>> CountAsync(CancellationToken cancellationToken = default);
}
