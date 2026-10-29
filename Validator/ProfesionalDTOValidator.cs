using DTO.Profesional;
using FluentValidation;

namespace Validator;

public class ProfesionalDTOValidator : AbstractValidator<ProfesionalDTO>
{
    public ProfesionalDTOValidator()
    {
        RuleFor(x => x.NegocioId)
            .GreaterThan(0).WithMessage("El ID del negocio debe ser válido.");

        RuleFor(x => x.NombreCompleto)
            .NotEmpty().WithMessage("El nombre completo es obligatorio.")
            .MaximumLength(150).WithMessage("El nombre completo no puede exceder 150 caracteres.");

        RuleFor(x => x.TituloProfesional)
            .NotEmpty().WithMessage("El título profesional es obligatorio.")
            .MaximumLength(100).WithMessage("El título profesional no puede exceder 100 caracteres.");

        RuleFor(x => x.ColorIdentificador)
            .NotEmpty().WithMessage("El color identificador es obligatorio.")
            .Matches("^#[0-9A-Fa-f]{6}$").WithMessage("El color identificador debe ser un formato Hexadecimal válido (#RRGGBB).");
    }
}
