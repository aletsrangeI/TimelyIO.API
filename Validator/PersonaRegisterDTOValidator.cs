using DTO.Persona;
using FluentValidation;

namespace Validator;

public class PersonaRegisterDTOValidator : AbstractValidator<PersonaRegisterDTO>
{
    public PersonaRegisterDTOValidator()
    {
        RuleFor(x => x.Nombres).NotNull().NotEmpty();
        RuleFor(x => x.Apellidos).NotNull().NotEmpty();
        RuleFor(x => x.Email).NotNull().NotEmpty().EmailAddress();
        RuleFor(x => x.Telefono).NotNull().NotEmpty();
        RuleFor(x => x.FechaNacimiento).NotNull().NotEmpty();
        RuleFor(x => x.TypeId).NotNull().NotEmpty();
    }
}

