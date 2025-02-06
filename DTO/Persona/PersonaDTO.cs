namespace DTO.Persona;

public class PersonaDTO
{
    public int Id { get; set; }
    public string Nombres { get; set; }
    public string Apellidos { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public string Telefono { get; set; }
    public DateTime FechaNacimiento { get; set; }
    public int TypeId { get; set; }
    public Dictionary<string, object> Metadata { get; set; } = new();
    public string Token { get; set; }
}