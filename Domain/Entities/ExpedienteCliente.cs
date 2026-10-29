namespace Domain.Entities;

public class ExpedienteCliente : BaseAuditableEntity
{
    public int NegocioId { get; set; }
    public Negocio Negocio { get; set; } = null!;

    public int ClienteId { get; set; }
    public Cliente Cliente { get; set; } = null!;

    public string CodigoExpediente { get; set; } = null!;
    public DateTime FechaAperturaUtc { get; set; } = DateTime.UtcNow;
}
