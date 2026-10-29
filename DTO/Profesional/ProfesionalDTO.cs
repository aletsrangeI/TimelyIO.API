namespace DTO.Profesional;

public class ProfesionalDTO
{
    public int Id { get; set; }
    public int NegocioId { get; set; }
    public int? UsuarioId { get; set; }
    public string NombreCompleto { get; set; } = null!;
    public string TituloProfesional { get; set; } = null!;
    public string? FotoUrl { get; set; }
    public string ColorIdentificador { get; set; } = "#1E3F35";
    public bool Active { get; set; } = true;
}
