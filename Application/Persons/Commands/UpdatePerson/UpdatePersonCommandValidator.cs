using FluentValidation;

namespace Movies.Application.Persons.Commands.UpdatePerson;

public class UpdatePersonCommandValidator : AbstractValidator<UpdatePersonCommand>
{
    public UpdatePersonCommandValidator()
    {
        RuleFor(p => p.FullName)
            .NotEmpty()
            .MaximumLength(100);
    }
}
