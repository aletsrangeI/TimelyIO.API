using DTO;
using FluentValidation;

namespace Validator;

public class CatalogoDTOValidator : AbstractValidator<CatalogoDTO>
{
    public CatalogoDTOValidator()
    {
        RuleFor(x => x.Nombre).NotNull().NotEmpty();
        RuleFor(x => x.Descripcion).NotNull().NotEmpty();
    }
}