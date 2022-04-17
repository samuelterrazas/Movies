namespace Movies.WebAPI.Controllers;

[Route("api/movies")]
public class MoviesController : ApiControllerBase
{
    [HttpGet]
    [Authorize(Roles = "Administrator, User")]
    public async Task<IActionResult> GetAll([FromQuery] GetMoviesQuery query) => Ok(await Mediator.Send(query));


    [HttpGet("{id:int}")]
    [Authorize(Roles = "Administrator, User")]
    public async Task<IActionResult> GetDetails(int id) => Ok(await Mediator.Send(new GetMovieDetailsQuery(id)));


    [HttpPost]
    [Authorize(Roles = "Administrator")]
    public async Task<IActionResult> Create([FromBody] CreateMovieCommand command) => Ok(await Mediator.Send(command));


    [HttpPut("{id:int}")]
    [Authorize(Roles = "Administrator")]
    public async Task<IActionResult> Update(int id, [FromBody] UpdateMovieCommand command)
    {
        command = command with {Id = id};
        await Mediator.Send(command);

        return NoContent();
    }

    
    [HttpDelete("{id:int}")]
    [Authorize(Roles = "Administrator")]
    public async Task<IActionResult> Delete(int id)
    {
        await Mediator.Send(new DeleteMovieCommand(id));

        return NoContent();
    }
}
