namespace Movies.Application.Auth.Commands.Login;

public class LoginCommandValidator : AbstractValidator<LoginCommand>
{
    public LoginCommandValidator()
    {
        RuleFor(p => p.Email)
            .NotEmpty()
            .EmailAddress();

        RuleFor(p => p.Password)
            .NotEmpty();
    }
}