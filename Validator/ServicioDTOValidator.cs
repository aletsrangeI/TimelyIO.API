using DTO.Servicio;
using FluentValidation;

namespace Validator;

public class ServicioDTOValidator : AbstractValidator<ServicioDTO>
{
    public ServicioDTOValidator()
    {
        RuleFor(x => x.NegocioId)
            .GreaterThan(0).WithMessage("El ID del negocio debe ser válido.");

        RuleFor(x => x.Nombre)
            .NotEmpty().WithMessage("El nombre del servicio es obligatorio.")
            .MaximumLength(150).WithMessage("El nombre no puede exceder 150 caracteres.");

        RuleFor(x => x.DuracionMinutos)
            .GreaterThan(0).WithMessage("La duración debe ser mayor a 0 minutos.");

        RuleFor(x => x.BufferMinutosPosterior)
            .GreaterThanOrEqualTo(0).WithMessage("El tiempo de holgura/limpieza no puede ser negativo.");

        RuleFor(x => x.PrecioTotal)
            .GreaterThanOrEqualTo(0).WithMessage("El precio total no puede ser negativo.");

        When(x => x.RequiereAnticipo, () =>
        {
            RuleFor(x => x.MontoAnticipo)
                .GreaterThan(0).WithMessage("El monto de anticipo debe ser mayor a 0 cuando el servicio lo requiera.")
                .LessThanOrEqualTo(x => x.PrecioTotal).WithMessage("El anticipo no puede ser mayor que el precio total.");
        });
    }
}
