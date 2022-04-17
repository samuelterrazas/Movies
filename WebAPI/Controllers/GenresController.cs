namespace Movies.WebAPI.Controllers;

[Route("api/genres")]
public class GenresController : ApiControllerBase
{
    [HttpGet]
    [Authorize(Roles = "Administrator, User")]
    public async Task<IActionResult> GetAll() => Ok(await Mediator.Send(new GetGenresQuery()));


    [HttpPost]
    [Authorize(Roles = "Administrator")]
    public async Task<IActionResult> Create([FromBody] CreateGenreCommand command) => Ok(await Mediator.Send(command));


    [HttpPut("{id:int}")]
    [Authorize(Roles = "Administrator")]
    public async Task<IActionResult> Update(int id, [FromBody] UpdateGenreCommand command)
    {
        command = command with {Id = id};
        await Mediator.Send(command);

        return NoContent();
    }

    
    [HttpDelete("{id:int}")]
    [Authorize(Roles = "Administrator")]
    public async Task<IActionResult> Delete(int id)
    {
        await Mediator.Send(new DeleteGenreCommand(id));

        return NoContent();
    }
}
