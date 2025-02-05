namespace Domain.Entities;

public class Catalogo : BaseAuditableEntity
{
    public string Nombre { get; set; }
    public string Descripcion { get; set; }
}