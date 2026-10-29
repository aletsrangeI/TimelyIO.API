namespace DTO.Cliente;

public class ClienteDTO
{
    public int Id { get; set; }
    public int NegocioId { get; set; }
    public string NombreCompleto { get; set; } = null!;
    public string TelefonoWhatsapp { get; set; } = null!;
    public string? Email { get; set; }
    public string? NotasGenerales { get; set; }
    public bool Active { get; set; } = true;
}
