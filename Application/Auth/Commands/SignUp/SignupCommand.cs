namespace Movies.Application.Auth.Commands.SignUp;

public record SignupCommand(string Email, string Password, string ConfirmPassword) : IRequest<Result>;

public class SignupCommandHandler : IRequestHandler<SignupCommand, Result>
{
    private readonly IIdentityService _identityService;
    private const string DefaultRole = "User";

    public SignupCommandHandler(IIdentityService identityService)
    {
        _identityService = identityService;
    }
        
    public async Task<Result> Handle(SignupCommand request, CancellationToken cancellationToken)
    {
        var emailExist = await _identityService.GetEmailAsync(request.Email);
            
        if (emailExist is not null)
            throw new BadRequestException("Email already exist.");
        
        var isCreated = await _identityService.CreateAsync(request.Email, request.Password);
        
        if (!isCreated.Succeeded)
            return Result.Failure(isCreated.Errors);

        var addToRole = await _identityService.AddToRoleAsync(request.Email, DefaultRole);
        
        if (!addToRole.Succeeded)
            return Result.Failure(addToRole.Errors);

        return Result.Success();
    }
}