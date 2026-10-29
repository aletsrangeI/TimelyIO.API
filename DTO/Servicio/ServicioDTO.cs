namespace DTO.Servicio;

public class ServicioDTO
{
    public int Id { get; set; }
    public int NegocioId { get; set; }
    public string Nombre { get; set; } = null!;
    public string? Descripcion { get; set; }
    public int DuracionMinutos { get; set; } = 45;
    public int BufferMinutosPosterior { get; set; } = 15;
    public decimal PrecioTotal { get; set; }
    public bool RequiereAnticipo { get; set; }
    public decimal MontoAnticipo { get; set; }
    public int? DiasParaReenganche { get; set; }
    public bool Active { get; set; } = true;
}
