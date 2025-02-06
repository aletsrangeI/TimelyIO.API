using DTO.Persona;
using FluentValidation;

namespace Validator;

public class PersonaDTOValidator : AbstractValidator<PersonaDTO>
{
    public PersonaDTOValidator()
    {
        RuleFor(x => x.Nombres).NotNull().NotEmpty();
        RuleFor(x => x.Apellidos).NotNull().NotEmpty();
        RuleFor(x => x.Email).NotNull().NotEmpty().EmailAddress();
        RuleFor(x => x.Password).NotNull().NotEmpty();
        RuleFor(x => x.Telefono).NotNull().NotEmpty();
        RuleFor(x => x.FechaNacimiento).NotNull().NotEmpty();
        RuleFor(x => x.TypeId).NotNull().NotEmpty();
    }
}