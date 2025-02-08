using DTO;
using DTO.ContenidoCatalogo;
using FluentValidation;

namespace Validator;

public class ContenidoCatalogoDTOValidator : AbstractValidator<ContenidoCatalogoDTO>
{
    public ContenidoCatalogoDTOValidator()
    {
        RuleFor(x => x.Nombre).NotNull().NotEmpty();
        RuleFor(x => x.Descripcion).NotNull().NotEmpty();
    }
}