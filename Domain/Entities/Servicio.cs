namespace Domain.Entities;

public class Servicio : BaseAuditableEntity
{
    public int NegocioId { get; set; }
    public Negocio Negocio { get; set; } = null!;

    public string Nombre { get; set; } = null!;
    public string? Descripcion { get; set; }
    public int DuracionMinutos { get; set; } = 45;
    public int BufferMinutosPosterior { get; set; } = 15;
    public decimal PrecioTotal { get; set; }
    public bool RequiereAnticipo { get; set; }
    public decimal MontoAnticipo { get; set; }
    public int? DiasParaReenganche { get; set; }

    // Relaciones
    public ICollection<Cita> Citas { get; set; } = new List<Cita>();
}
