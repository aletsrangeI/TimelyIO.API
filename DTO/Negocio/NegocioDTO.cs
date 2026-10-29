namespace DTO.Negocio;

public class NegocioDTO
{
    public int Id { get; set; }
    public string NombreComercial { get; set; } = null!;
    public string Slug { get; set; } = null!;
    public string TelefonoWhatsappOficial { get; set; } = null!;
    public string? DatosBancariosSPEI { get; set; }
    public int PoliticaCancelacionHoras { get; set; } = 24;
    public string ZonaHorariaId { get; set; } = "America/Mexico_City";
    public TimeSpan HorarioAtencionInicio { get; set; } = new TimeSpan(9, 0, 0);
    public TimeSpan HorarioAtencionFin { get; set; } = new TimeSpan(19, 0, 0);
    public string? MensajeFueraHorario { get; set; }
    public bool Active { get; set; } = true;
}
