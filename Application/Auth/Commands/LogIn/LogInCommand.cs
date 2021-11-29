using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Movies.Application.Common.Exceptions;
using Movies.Application.Common.Interfaces;
using Movies.Application.Common.Models;

namespace Movies.Application.Auth.Commands.LogIn
{
    public class LogInCommand : IRequest<Result>
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }

    public class LogInCommandHandler : IRequestHandler<LogInCommand, Result>
    {
        private readonly IIdentityService _identityService;
        private readonly ITokenHandlerService _tokenHandlerService;

        public LogInCommandHandler(IIdentityService identityService, ITokenHandlerService tokenHandlerService)
        {
            _identityService = identityService;
            _tokenHandlerService = tokenHandlerService;
        }
        
        public async Task<Result> Handle(LogInCommand request, CancellationToken cancellationToken)
        {
            var userExist = await _identityService.GetEmailAsync(request.Email);

            if (userExist is null)
                throw new BadRequestException("Incorrect email or password.");

            var isCorrect = await _identityService.CheckUserPasswordAsync(userExist, request.Password);

            if (!isCorrect)
                throw new BadRequestException("Incorrect email or password.");

            var parameters = await _identityService.GenerateTokenParametersAsync(userExist);

            var jwtToken = _tokenHandlerService.GenerateJwtToken(parameters);

            return Result.LoggedIn(jwtToken);
        }
    }
}