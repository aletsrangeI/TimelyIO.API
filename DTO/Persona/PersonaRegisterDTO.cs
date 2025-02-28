namespace DTO.Persona;

public class PersonaRegisterDTO
{
    public int Id { get; set; }
    public string Nombres { get; set; }
    public string Apellidos { get; set; }
    public string Email { get; set; }
    public string Telefono { get; set; }
    public DateTime FechaNacimiento { get; set; }
    public int TypeId { get; set; }
}