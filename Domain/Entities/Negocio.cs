namespace Domain.Entities;

public class Negocio : BaseAuditableEntity
{
    public string NombreComercial { get; set; } = null!;
    public string Slug { get; set; } = null!;
    public string TelefonoWhatsappOficial { get; set; } = null!;
    public string? DatosBancariosSPEI { get; set; }
    public int PoliticaCancelacionHoras { get; set; } = 24;
    public string ZonaHorariaId { get; set; } = "America/Mexico_City";
    public TimeSpan HorarioAtencionInicio { get; set; } = new TimeSpan(9, 0, 0);
    public TimeSpan HorarioAtencionFin { get; set; } = new TimeSpan(19, 0, 0);
    public string? MensajeFueraHorario { get; set; }

    // Relaciones
    public ICollection<Profesional> Profesionales { get; set; } = new List<Profesional>();
    public ICollection<Servicio> Servicios { get; set; } = new List<Servicio>();
    public ICollection<Cliente> Clientes { get; set; } = new List<Cliente>();
    public ICollection<Cita> Citas { get; set; } = new List<Cita>();
}
