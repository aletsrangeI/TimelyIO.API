using DTO.Cliente;
using ErrorOr;

namespace Interface.UseCases;

/// <summary>
/// Contrato de aplicacion asincrono puro gobernado por el patron Result (ErrorOr).
/// Elimina el lanzamiento de excepciones para flujos de error semanticos.
/// </summary>
public interface IClienteApplication
{
    Task<ErrorOr<bool>> InsertAsync(ClienteDTO dto, CancellationToken cancellationToken = default);
    Task<ErrorOr<bool>> UpdateAsync(ClienteDTO dto, CancellationToken cancellationToken = default);
    Task<ErrorOr<bool>> DeleteAsync(int id, CancellationToken cancellationToken = default);
    Task<ErrorOr<ClienteDTO>> GetAsync(int id, CancellationToken cancellationToken = default);
    Task<ErrorOr<IEnumerable<ClienteDTO>>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<ErrorOr<IEnumerable<ClienteDTO>>> GetAllWithPaginationAsync(int page, int pageSize, CancellationToken cancellationToken = default);
    Task<ErrorOr<int>> CountAsync(CancellationToken cancellationToken = default);
}
