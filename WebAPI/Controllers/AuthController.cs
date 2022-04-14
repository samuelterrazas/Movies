using Microsoft.AspNetCore.Mvc;
using Movies.Application.Auth.Commands.Login;
using Movies.Application.Auth.Commands.Signup;

namespace Movies.WebAPI.Controllers;

[Route("api/auth")]
public class AuthController : ApiControllerBase
{
    [HttpPost("signup")]
    public async Task<IActionResult> Signup([FromBody] SignupCommand command) => Ok(await Mediator.Send(command));


    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginCommand command) => Ok(await Mediator.Send(command));
}
