using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities;

public class ContenidoCatalogo : BaseAuditableEntity
{
    public string Nombre { get; set; }
    public string Descripcion { get; set; }
    public string Opcional { get; set; }
    [ForeignKey("Catalogo")] public int CatalogoId { get; set; }
    public Catalogo Catalogo { get; set; }
}