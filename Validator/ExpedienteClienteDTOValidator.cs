using DTO.ExpedienteCliente;
using FluentValidation;

namespace Validator;

public class ExpedienteClienteDTOValidator : AbstractValidator<ExpedienteClienteDTO>
{
    public ExpedienteClienteDTOValidator()
    {
        RuleFor(x => x.NegocioId)
            .GreaterThan(0).WithMessage("El ID del negocio debe ser válido.");

        RuleFor(x => x.ClienteId)
            .GreaterThan(0).WithMessage("El ID del cliente debe ser válido.");

        RuleFor(x => x.CodigoExpediente)
            .NotEmpty().WithMessage("El código del expediente es obligatorio.")
            .MaximumLength(20).WithMessage("El código no puede exceder 20 caracteres.");
    }
}
