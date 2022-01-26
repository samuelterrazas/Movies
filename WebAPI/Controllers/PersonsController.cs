using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Movies.Common.DTOs;
using Movies.Application.Persons.Commands.CreatePerson;
using Movies.Application.Persons.Commands.DeletePerson;
using Movies.Application.Persons.Commands.UpdatePerson;
using Movies.Application.Persons.Queries.GetPersons;
using Movies.Application.Persons.Queries.GetPersonDetails;
using Movies.Common.Wrappers;

namespace Movies.WebAPI.Controllers;

[Route("api/persons")]
public class PersonsController : ApiControllerBase
{
    [HttpGet]
    //[Authorize(Roles = "Administrator, User")]
    public async Task<ActionResult<PaginatedResponse<PersonsDto>>> GetAll([FromQuery] GetPersonsQuery query)
    {
        return await Mediator.Send(query);
    }

    
    [HttpGet("{id}")]
    //[Authorize(Roles = "Administrator")]
    public async Task<ActionResult<PersonDetailsDto>> GetDetails(int id)
    {
        return await Mediator.Send(new GetPersonDetailsQuery(id));
    }

    
    [HttpPost]
    //[Authorize(Roles = "Administrator")]
    public async Task<ActionResult<int>> Create([FromBody] CreatePersonCommand command)
    {
        return await Mediator.Send(command);
    }

    
    [HttpPut("{id}")]
    //[Authorize(Roles = "Administrator")]
    public async Task<ActionResult> Update(int id, [FromBody] UpdatePersonCommand command)
    {
        await Mediator.Send(command);

        return NoContent();
    }

    
    [HttpDelete("{id}")]
    //[Authorize(Roles = "Administrator")]
    public async Task<ActionResult> Delete(int id)
    {
        await Mediator.Send(new DeletePersonCommand(id));

        return NoContent();
    }
}
