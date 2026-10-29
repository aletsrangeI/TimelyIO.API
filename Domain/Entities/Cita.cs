using Domain.Common;
using Domain.Enums;

namespace Domain.Entities;

public class Cita : BaseAuditableEntity
{
    public int NegocioId { get; set; }
    public Negocio Negocio { get; set; } = null!;

    public int ClienteId { get; set; }
    public Cliente Cliente { get; set; } = null!;

    public int ProfesionalId { get; set; }
    public Profesional Profesional { get; set; } = null!;

    public int ServicioId { get; set; }
    public Servicio Servicio { get; set; } = null!;

    public DateTime FechaHoraInicioUtc { get; set; }
    public DateTime FechaHoraFinUtc { get; set; }
    public DateTime FechaHoraFinConBufferUtc { get; set; }

    public EstadoCita Estado { get; set; } = EstadoCita.PendienteValidacionPago;
    public string? MotivoCancelacion { get; set; }
    public OrigenCancelacion? CanceladoPor { get; set; }

    public decimal CostoTotal { get; set; }
    public decimal MontoAnticipoRequerido { get; set; }
    public Guid TokenAccesoCliente { get; set; } = Guid.NewGuid();

    /// <summary>
    /// Retorna el intervalo de tiempo con buffer para verificaciones de no-traslape.
    /// </summary>
    public IntervaloTiempo ObtenerIntervaloConBuffer() =>
        new IntervaloTiempo(FechaHoraInicioUtc, FechaHoraFinConBufferUtc);
}
