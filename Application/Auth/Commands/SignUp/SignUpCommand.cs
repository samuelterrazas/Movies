using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Movies.Application.Common.Exceptions;
using Movies.Application.Common.Interfaces;
using Movies.Application.Common.Models;

namespace Movies.Application.Auth.Commands.SignUp
{
    public class SignUpCommand : IRequest<Result>
    {
        public string Email { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
    }

    public class SignUpCommandHandler : IRequestHandler<SignUpCommand, Result>
    {
        private readonly IIdentityService _identityService;

        public SignUpCommandHandler(IIdentityService identityService)
        {
            _identityService = identityService;
        }
        
        public async Task<Result> Handle(SignUpCommand request, CancellationToken cancellationToken)
        {
            var emailExist = await _identityService.GetEmailAsync(request.Email);
            if (emailExist is not null)
                throw new BadRequestException("Email already exist.");

            var userNameExist = await _identityService.GetUserNameAsync(request.UserName);
            if (userNameExist is not null)
                throw new BadRequestException("Username already exist.");
            
            await _identityService.CreateUserAsync(request.Email, request.UserName, request.Password);

            return Result.Success();
        }
    }
}