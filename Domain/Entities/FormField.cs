using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities;

public class FormField : BaseAuditableEntity
{
    public string Type { get; set; }
    public string Name { get; set; }
    public string Placeholder { get; set; }
    public string Label { get; set; }
    public string Value { get; set; }
    public List<FormValidation> Validations { get; set; } = new();
    public List<SelectFormOption>? Options { get; set; } = new();
    [ForeignKey("ContenidoCatalogo")] public int FormularioId { get; set; }
    public ContenidoCatalogo Formulario { get; set; }
    [ForeignKey("Catalogo")] public int? CatalogoId { get; set; }
    public Catalogo Catalogo { get; set; }
    public int Order { get; set; }
}