using DTO.Cliente;
using FluentValidation;

namespace Validator;

public class ClienteDTOValidator : AbstractValidator<ClienteDTO>
{
    public ClienteDTOValidator()
    {
        RuleFor(x => x.NegocioId)
            .GreaterThan(0).WithMessage("El ID del negocio debe ser válido.");

        RuleFor(x => x.NombreCompleto)
            .NotEmpty().WithMessage("El nombre completo es obligatorio.")
            .MaximumLength(150).WithMessage("El nombre no puede exceder 150 caracteres.");

        RuleFor(x => x.TelefonoWhatsapp)
            .NotEmpty().WithMessage("El teléfono WhatsApp es obligatorio.")
            .MaximumLength(20).WithMessage("El teléfono no puede exceder 20 caracteres.");

        When(x => !string.IsNullOrEmpty(x.Email), () =>
        {
            RuleFor(x => x.Email)
                .EmailAddress().WithMessage("El formato del correo electrónico no es válido.")
                .MaximumLength(100).WithMessage("El correo electrónico no puede exceder 100 caracteres.");
        });
    }
}
