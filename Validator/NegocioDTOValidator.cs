using DTO.Negocio;
using FluentValidation;

namespace Validator;

public class NegocioDTOValidator : AbstractValidator<NegocioDTO>
{
    public NegocioDTOValidator()
    {
        RuleFor(x => x.NombreComercial)
            .NotEmpty().WithMessage("El nombre comercial es obligatorio.")
            .MaximumLength(150).WithMessage("El nombre comercial no puede exceder 150 caracteres.");

        RuleFor(x => x.Slug)
            .NotEmpty().WithMessage("El slug es obligatorio.")
            .MaximumLength(50).WithMessage("El slug no puede exceder 50 caracteres.")
            .Matches("^[a-z0-9-]+$").WithMessage("El slug solo puede contener letras minúsculas, números y guiones.");

        RuleFor(x => x.TelefonoWhatsappOficial)
            .NotEmpty().WithMessage("El teléfono WhatsApp oficial es obligatorio.")
            .MaximumLength(20).WithMessage("El teléfono WhatsApp no puede exceder 20 caracteres.");
    }
}
