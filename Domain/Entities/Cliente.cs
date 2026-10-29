namespace Domain.Entities;

public class Cliente : BaseAuditableEntity
{
    public int NegocioId { get; set; }
    public Negocio Negocio { get; set; } = null!;

    public string NombreCompleto { get; set; } = null!;
    public string TelefonoWhatsapp { get; set; } = null!;
    public string? Email { get; set; }
    public string? NotasGenerales { get; set; }

    // Relaciones
    public ICollection<Cita> Citas { get; set; } = new List<Cita>();
    public ExpedienteCliente? Expediente { get; set; }
}
