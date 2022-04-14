using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Movies.Application.Persons.Commands.CreatePerson;
using Movies.Application.Persons.Commands.DeletePerson;
using Movies.Application.Persons.Commands.UpdatePerson;
using Movies.Application.Persons.Queries.GetPersons;
using Movies.Application.Persons.Queries.GetPersonDetails;

namespace Movies.WebAPI.Controllers;

[Route("api/persons")]
public class PersonsController : ApiControllerBase
{
    [HttpGet]
    [Authorize(Roles = "Administrator, User")]
    public async Task<IActionResult> GetAll([FromQuery] GetPersonsQuery query) => Ok(await Mediator.Send(query));


    [HttpGet("{id:int}")]
    [Authorize(Roles = "Administrator")]
    public async Task<IActionResult> GetDetails(int id) => Ok(await Mediator.Send(new GetPersonDetailsQuery(id)));


    [HttpPost]
    [Authorize(Roles = "Administrator")]
    public async Task<IActionResult> Create([FromBody] CreatePersonCommand command) => Ok(await Mediator.Send(command));


    [HttpPut("{id:int}")]
    [Authorize(Roles = "Administrator")]
    public async Task<IActionResult> Update(int id, [FromBody] UpdatePersonCommand command)
    {
        command = command with {Id = id};
        await Mediator.Send(command);

        return NoContent();
    }

    
    [HttpDelete("{id:int}")]
    [Authorize(Roles = "Administrator")]
    public async Task<IActionResult> Delete(int id)
    {
        await Mediator.Send(new DeletePersonCommand(id));

        return NoContent();
    }
}
