using FluentValidation;

namespace Movies.Application.Persons.Commands.CreatePerson;

public class CreatePersonCommandValidator : AbstractValidator<CreatePersonCommand>
{
    public CreatePersonCommandValidator()
    {
        RuleFor(p => p.FullName)
            .NotEmpty()
            .MaximumLength(100);
    }
}