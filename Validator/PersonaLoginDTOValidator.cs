using DTO.Persona;
using FluentValidation;

namespace Validator;

public class PersonaLoginDTOValidator : AbstractValidator<PersonaLoginDTO>
{
    public PersonaLoginDTOValidator()
    {
        RuleFor(x => x.Email).NotNull().NotEmpty().EmailAddress();
        RuleFor(x => x.Password).NotNull().NotEmpty();
    }
}