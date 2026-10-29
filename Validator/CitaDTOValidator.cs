using DTO.Cita;
using FluentValidation;

namespace Validator;

public class CitaDTOValidator : AbstractValidator<CitaDTO>
{
    public CitaDTOValidator()
    {
        RuleFor(x => x.NegocioId)
            .GreaterThan(0).WithMessage("El ID del negocio debe ser válido.");

        RuleFor(x => x.ClienteId)
            .GreaterThan(0).WithMessage("El ID del cliente debe ser válido.");

        RuleFor(x => x.ProfesionalId)
            .GreaterThan(0).WithMessage("El ID del profesional debe ser válido.");

        RuleFor(x => x.ServicioId)
            .GreaterThan(0).WithMessage("El ID del servicio debe ser válido.");

        RuleFor(x => x.FechaHoraInicioUtc)
            .NotEmpty().WithMessage("La fecha y hora de inicio son requeridas.");

        RuleFor(x => x.FechaHoraFinUtc)
            .GreaterThan(x => x.FechaHoraInicioUtc)
            .WithMessage("La fecha de fin debe ser posterior a la fecha de inicio.");

        RuleFor(x => x.FechaHoraFinConBufferUtc)
            .GreaterThanOrEqualTo(x => x.FechaHoraFinUtc)
            .WithMessage("La fecha de fin con buffer debe ser mayor o igual a la fecha de fin.");

        RuleFor(x => x.CostoTotal)
            .GreaterThanOrEqualTo(0).WithMessage("El costo total no puede ser negativo.");

        RuleFor(x => x.MontoAnticipoRequerido)
            .GreaterThanOrEqualTo(0).WithMessage("El monto de anticipo no puede ser negativo.")
            .LessThanOrEqualTo(x => x.CostoTotal).WithMessage("El anticipo no puede ser mayor al costo total.");
    }
}
