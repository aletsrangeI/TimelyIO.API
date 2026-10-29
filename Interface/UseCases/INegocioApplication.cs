using DTO.Negocio;
using ErrorOr;

namespace Interface.UseCases;

/// <summary>
/// Contrato de aplicacion asincrono puro gobernado por el patron Result (ErrorOr).
/// Elimina el lanzamiento de excepciones para flujos de error semanticos.
/// </summary>
public interface INegocioApplication
{
    Task<ErrorOr<bool>> InsertAsync(NegocioDTO dto, CancellationToken cancellationToken = default);
    Task<ErrorOr<bool>> UpdateAsync(NegocioDTO dto, CancellationToken cancellationToken = default);
    Task<ErrorOr<bool>> DeleteAsync(int id, CancellationToken cancellationToken = default);
    Task<ErrorOr<NegocioDTO>> GetAsync(int id, CancellationToken cancellationToken = default);
    Task<ErrorOr<IEnumerable<NegocioDTO>>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<ErrorOr<IEnumerable<NegocioDTO>>> GetAllWithPaginationAsync(int page, int pageSize, CancellationToken cancellationToken = default);
    Task<ErrorOr<int>> CountAsync(CancellationToken cancellationToken = default);
}
