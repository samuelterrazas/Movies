using Microsoft.AspNetCore.Mvc;
using Movies.Application.Auth.Commands.Login;
using Movies.Application.Auth.Commands.Signup;
using Movies.Common.Wrappers;

namespace Movies.WebAPI.Controllers;

[Route("api/auth")]
public class AuthController : ApiControllerBase
{
    [HttpPost("signup")]
    public async Task<ActionResult<Result>> Signup([FromBody] SignupCommand command)
    {
        return await Mediator.Send(command);
    }
        
    
    [HttpPost("login")]
    public async Task<ActionResult<Result>> Login([FromBody] LoginCommand command)
    {
        return await Mediator.Send(command);
    }
}
