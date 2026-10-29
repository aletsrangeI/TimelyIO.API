using DTO.Cita;
using ErrorOr;

namespace Interface.UseCases;

/// <summary>
/// Contrato de aplicacion asincrono puro gobernado por el patron Result (ErrorOr).
/// Elimina el lanzamiento de excepciones para flujos de error semanticos.
/// </summary>
public interface ICitaApplication
{
    Task<ErrorOr<bool>> InsertAsync(CitaDTO dto, CancellationToken cancellationToken = default);
    Task<ErrorOr<bool>> UpdateAsync(CitaDTO dto, CancellationToken cancellationToken = default);
    Task<ErrorOr<bool>> DeleteAsync(int id, CancellationToken cancellationToken = default);
    Task<ErrorOr<CitaDTO>> GetAsync(int id, CancellationToken cancellationToken = default);
    Task<ErrorOr<IEnumerable<CitaDTO>>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<ErrorOr<IEnumerable<CitaDTO>>> GetAllWithPaginationAsync(int page, int pageSize, CancellationToken cancellationToken = default);
    Task<ErrorOr<int>> CountAsync(CancellationToken cancellationToken = default);
    Task<ErrorOr<CitaDTO>> GetByTokenAsync(Guid token, CancellationToken cancellationToken = default);
    Task<ErrorOr<bool>> AccionClienteAsync(Guid token, string accion, CancellationToken cancellationToken = default);
    Task<ErrorOr<PublicReservaResponseDTO>> RegistrarReservaPublicaAsync(PublicReservaDTO dto, string? clientIp = null, CancellationToken cancellationToken = default);
}
