using Domain.Enums;

namespace DTO.Cita;

public class PublicReservaResponseDTO
{
    public int Id { get; set; }
    public Guid TokenAccesoCliente { get; set; }
    public DateTime FechaHoraInicioUtc { get; set; }
    public DateTime FechaHoraFinUtc { get; set; }
    public EstadoCita Estado { get; set; }
    public decimal CostoTotal { get; set; }
    public bool RequiereAnticipo { get; set; }
    public decimal MontoAnticipo { get; set; }
    public string NombreServicio { get; set; } = string.Empty;
    public string NombreProfesional { get; set; } = string.Empty;
}
