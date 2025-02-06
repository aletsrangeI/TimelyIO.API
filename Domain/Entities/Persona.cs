using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities;

public class Persona : BaseAuditableEntity
{
    public string Nombres { get; set; }
    public string Apellidos { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public string Telefono { get; set; }
    public DateTime FechaNacimiento { get; set; }
    [ForeignKey("ContenidoCatalogo")] public int TypeId { get; set; }
    public ContenidoCatalogo Type { get; set; }
    public Dictionary<string, object> Metadata { get; set; } = new();
}