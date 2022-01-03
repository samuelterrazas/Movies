using Movies.Application.Common.Wrappers;

namespace Movies.Application.Auth.Commands.SignUp;

public class SignupCommand : IRequest<Result>
{
    public string Email { get; set; }
    public string Password { get; set; }
    public string ConfirmPassword { get; set; }
}

public class SignupCommandHandler : IRequestHandler<SignupCommand, Result>
{
    private readonly IIdentityService _identityService;

    public SignupCommandHandler(IIdentityService identityService)
    {
        _identityService = identityService;
    }
        
    public async Task<Result> Handle(SignupCommand request, CancellationToken cancellationToken)
    {
        var emailExist = await _identityService.GetEmailAsync(request.Email);
            
        if (emailExist is not null)
            throw new BadRequestException("Email already exist.");

        await _identityService.CreateUserAsync(request.Email, request.Password);

        return Result.Success();
    }
}