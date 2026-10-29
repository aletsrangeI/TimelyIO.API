namespace DTO.Cita;

public class PublicReservaDTO
{
    public int NegocioId { get; set; }
    public int ServicioId { get; set; }
    public int? ProfesionalId { get; set; }
    public DateTime FechaHoraInicioUtc { get; set; }
    public string NombreCliente { get; set; } = string.Empty;
    public string TelefonoWhatsapp { get; set; } = string.Empty;
    public string? Email { get; set; }
    public string? Notas { get; set; }
    public string? TurnstileToken { get; set; }
    public string? Honeypot { get; set; }
}
