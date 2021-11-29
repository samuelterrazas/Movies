using FluentValidation;

namespace Movies.Application.Auth.Commands.LogIn
{
    public class LogInCommandValidator : AbstractValidator<LogInCommand>
    {
        public LogInCommandValidator()
        {
            RuleFor(l => l.Email)
                .NotEmpty()
                .EmailAddress();

            RuleFor(l => l.Password)
                .NotEmpty();
        }
    }
}