using Domain.Enums;

namespace DTO.Cita;

public class CitaDTO
{
    public int Id { get; set; }
    public int NegocioId { get; set; }
    public int ClienteId { get; set; }
    public int ProfesionalId { get; set; }
    public int ServicioId { get; set; }
    public DateTime FechaHoraInicioUtc { get; set; }
    public DateTime FechaHoraFinUtc { get; set; }
    public DateTime FechaHoraFinConBufferUtc { get; set; }
    public EstadoCita Estado { get; set; } = EstadoCita.PendienteValidacionPago;
    public string? MotivoCancelacion { get; set; }
    public OrigenCancelacion? CanceladoPor { get; set; }
    public decimal CostoTotal { get; set; }
    public decimal MontoAnticipoRequerido { get; set; }
    public Guid TokenAccesoCliente { get; set; }
    public bool Active { get; set; } = true;
}
