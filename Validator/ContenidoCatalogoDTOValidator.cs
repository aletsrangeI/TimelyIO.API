using DTO;
using FluentValidation;

namespace Validator;

public class ContenidoCatalogoDTOValidator : AbstractValidator<CatalogoDTO>
{
    public ContenidoCatalogoDTOValidator()
    {
        RuleFor(x => x.Nombre).NotNull().NotEmpty();
        RuleFor(x => x.Descripcion).NotNull().NotEmpty();
    }
}