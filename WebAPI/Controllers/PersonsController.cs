using System.Net;
using Microsoft.AspNetCore.Mvc;
using Movies.Common.DTOs;
using Movies.Application.Persons.Commands.CreatePerson;
using Movies.Application.Persons.Commands.DeletePerson;
using Movies.Application.Persons.Commands.UpdatePerson;
using Movies.Application.Persons.Queries.GetPersons;
using Movies.Application.Persons.Queries.GetPersonDetails;
using Movies.Common.Wrappers;
using NSwag.Annotations;

namespace Movies.WebAPI.Controllers;

[Route("api/persons")]
public class PersonsController : ApiControllerBase
{
    #region Documentation
    [SwaggerResponse((int)HttpStatusCode.OK, typeof(PaginatedResponse<PersonsDto>))]
    [SwaggerResponse((int)HttpStatusCode.Unauthorized, typeof(ProblemDetails))]
    [SwaggerResponse((int)HttpStatusCode.InternalServerError, typeof(ProblemDetails))]
    #endregion
    [HttpGet]
    //[Authorize(Roles = "Administrator, User")]
    public async Task<ActionResult<PaginatedResponse<PersonsDto>>> GetAll([FromQuery] GetPersonsQuery query)
    {
        return await Mediator.Send(query);
    }

    
    #region Documentation
    [SwaggerResponse((int)HttpStatusCode.OK, typeof(PersonDetailsDto))]
    [SwaggerResponse((int)HttpStatusCode.NotFound, typeof(ProblemDetails))]
    [SwaggerResponse((int)HttpStatusCode.Unauthorized, typeof(ProblemDetails))]
    [SwaggerResponse((int)HttpStatusCode.Forbidden, typeof(ProblemDetails))]
    [SwaggerResponse((int)HttpStatusCode.InternalServerError, typeof(ProblemDetails))]
    #endregion
    [HttpGet("{id}")]
    //[Authorize(Roles = "Administrator")]
    public async Task<ActionResult<PersonDetailsDto>> GetDetails(int id)
    {
        return await Mediator.Send(new GetPersonDetailsQuery(id));
    }


    #region Documentation
    [SwaggerResponse((int)HttpStatusCode.OK, typeof(int))]
    [SwaggerResponse((int)HttpStatusCode.BadRequest, typeof(ProblemDetails))]
    [SwaggerResponse((int)HttpStatusCode.Unauthorized, typeof(ProblemDetails))]
    [SwaggerResponse((int)HttpStatusCode.Forbidden, typeof(ProblemDetails))]
    [SwaggerResponse((int)HttpStatusCode.InternalServerError, typeof(ProblemDetails))]
    #endregion
    [HttpPost]
    //[Authorize(Roles = "Administrator")]
    public async Task<ActionResult<int>> Create([FromBody] CreatePersonCommand command)
    {
        return await Mediator.Send(command);
    }


    #region Documentation
    [SwaggerResponse((int)HttpStatusCode.NoContent, typeof(NoContentResult))]
    [SwaggerResponse((int)HttpStatusCode.BadRequest, typeof(ProblemDetails))]
    [SwaggerResponse((int)HttpStatusCode.NotFound, typeof(ProblemDetails))]
    [SwaggerResponse((int)HttpStatusCode.Unauthorized, typeof(ProblemDetails))]
    [SwaggerResponse((int)HttpStatusCode.Forbidden, typeof(ProblemDetails))]
    [SwaggerResponse((int)HttpStatusCode.InternalServerError, typeof(ProblemDetails))]
    #endregion
    [HttpPut("{id}")]
    //[Authorize(Roles = "Administrator")]
    public async Task<ActionResult> Update(int id, [FromBody] UpdatePersonCommand command)
    {
        await Mediator.Send(command);

        return NoContent();
    }


    #region Documentation
    [SwaggerResponse((int)HttpStatusCode.NoContent, typeof(NoContentResult))]
    [SwaggerResponse((int)HttpStatusCode.NotFound, typeof(ProblemDetails))]
    [SwaggerResponse((int)HttpStatusCode.Unauthorized, typeof(ProblemDetails))]
    [SwaggerResponse((int)HttpStatusCode.Forbidden, typeof(ProblemDetails))]
    [SwaggerResponse((int)HttpStatusCode.InternalServerError, typeof(ProblemDetails))]
    #endregion
    [HttpDelete("{id}")]
    //[Authorize(Roles = "Administrator")]
    public async Task<ActionResult> Delete(int id)
    {
        await Mediator.Send(new DeletePersonCommand(id));

        return NoContent();
    }
}
