namespace Movies.Application.Auth.Commands.Signup;

public class SignupCommandValidator : AbstractValidator<SignupCommand>
{
    public SignupCommandValidator()
    {
        RuleFor(p => p.Email)
            .NotEmpty()
            .EmailAddress();

        RuleFor(p => p.Password)
            .NotEmpty()
            .Matches("[A-Z]").WithMessage("'{PropertyName}' must have at least one uppercase ('A'-'Z').")
            .Matches("[a-z]").WithMessage("'{PropertyName}' must have at least one lowercase ('a'-'z').")
            .Matches("[0-9]").WithMessage("'{PropertyName}' must have at least one digit ('0'-'9').")
            .Matches("[^a-zA-Z0-9]").WithMessage("'{PropertyName}' must have at least one non alphanumeric character.")
            .MinimumLength(6).WithMessage("'{PropertyName}' must be at least 6 characters.")
            .MaximumLength(16).WithMessage("'{PropertyName}' must be a maximum of 16 characters.");

        RuleFor(p => p.ConfirmPassword)
            .NotEmpty()
            .Equal(p => p.Password).WithMessage("The password and confirmation password do not match.");
    }
}