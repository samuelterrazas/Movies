﻿namespace Movies.Application.Auth.Commands.Login;

public record LoginCommand(string Email, string Password) : IRequest<Result>;


public class LoginCommandHandler : IRequestHandler<LoginCommand, Result>
{
    private readonly IIdentityService _identityService;
    private readonly ITokenHandlerService _tokenHandlerService;

    
    public LoginCommandHandler(IIdentityService identityService, ITokenHandlerService tokenHandlerService)
    {
        _identityService = identityService;
        _tokenHandlerService = tokenHandlerService;
    }
    
    
    public async Task<Result> Handle(LoginCommand request, CancellationToken cancellationToken)
    {
        var userExist = await _identityService.GetEmailAsync(request.Email);
        
        if (userExist is null)
            throw new BadRequestException("Incorrect email or password.");

        var isCorrect = await _identityService.CheckPasswordAsync(userExist, request.Password);
        
        if (!isCorrect)
            throw new BadRequestException("Incorrect email or password.");

        var parameters = await _identityService.GenerateTokenParametersAsync(userExist);

        var jwtToken = _tokenHandlerService.GenerateJwtToken(parameters);
        
        return Result.LoggedIn(jwtToken);
    }
}