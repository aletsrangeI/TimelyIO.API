namespace DTO.ExpedienteCliente;

public class ExpedienteClienteDTO
{
    public int Id { get; set; }
    public int NegocioId { get; set; }
    public int ClienteId { get; set; }
    public string CodigoExpediente { get; set; } = null!;
    public DateTime FechaAperturaUtc { get; set; }
    public bool Active { get; set; } = true;
}
