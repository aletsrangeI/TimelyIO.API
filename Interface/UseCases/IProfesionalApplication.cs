using DTO.Profesional;
using ErrorOr;

namespace Interface.UseCases;

/// <summary>
/// Contrato de aplicacion asincrono puro gobernado por el patron Result (ErrorOr).
/// Elimina el lanzamiento de excepciones para flujos de error semanticos.
/// </summary>
public interface IProfesionalApplication
{
    Task<ErrorOr<bool>> InsertAsync(ProfesionalDTO dto, CancellationToken cancellationToken = default);
    Task<ErrorOr<bool>> UpdateAsync(ProfesionalDTO dto, CancellationToken cancellationToken = default);
    Task<ErrorOr<bool>> DeleteAsync(int id, CancellationToken cancellationToken = default);
    Task<ErrorOr<ProfesionalDTO>> GetAsync(int id, CancellationToken cancellationToken = default);
    Task<ErrorOr<IEnumerable<ProfesionalDTO>>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<ErrorOr<IEnumerable<ProfesionalDTO>>> GetAllWithPaginationAsync(int page, int pageSize, CancellationToken cancellationToken = default);
    Task<ErrorOr<int>> CountAsync(CancellationToken cancellationToken = default);
}
