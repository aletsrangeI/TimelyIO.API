namespace Domain.Entities;

public class Profesional : BaseAuditableEntity
{
    public int NegocioId { get; set; }
    public Negocio Negocio { get; set; } = null!;

    public int? UsuarioId { get; set; }
    public string NombreCompleto { get; set; } = null!;
    public string TituloProfesional { get; set; } = null!;
    public string? FotoUrl { get; set; }
    public string ColorIdentificador { get; set; } = "#1E3F35"; // Cypress green por defecto

    // Relaciones
    public ICollection<Cita> Citas { get; set; } = new List<Cita>();
}
