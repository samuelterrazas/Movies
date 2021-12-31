using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Movies.WebAPI.Controllers;

[ApiController]
public class ApiControllerBase : ControllerBase
{
    private ISender _mediator;

    protected ISender Mediator => _mediator ??= HttpContext.RequestServices.GetService<ISender>();
}
