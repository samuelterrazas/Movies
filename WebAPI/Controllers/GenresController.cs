using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Movies.Common.DTOs;
using Movies.Application.Genres.Commands.CreateGenre;
using Movies.Application.Genres.Commands.DeleteGenre;
using Movies.Application.Genres.Commands.UpdateGenre;
using Movies.Application.Genres.Queries.GetGenres;

namespace Movies.WebAPI.Controllers;

[Route("api/genres")]
public class GenresController : ApiControllerBase
{
    [HttpGet]
    //[Authorize(Roles = "Administrator, User")]
    public async Task<ActionResult<List<GenresDto>>> GetAll()
    {
        return await Mediator.Send(new GetGenresQuery());
    }

    
    [HttpPost]
    //[Authorize(Roles = "Administrator")]
    public async Task<ActionResult<int>> Create([FromBody] CreateGenreCommand command)
    {
        return await Mediator.Send(command);
    }

    
    [HttpPut("{id}")]
    //[Authorize(Roles = "Administrator")]
    public async Task<ActionResult> Update(int id, [FromBody] UpdateGenreCommand command)
    {
        command = command with {Id = id};
        await Mediator.Send(command);

        return NoContent();
    }

    
    [HttpDelete("{id}")]
    //[Authorize(Roles = "Administrator")]
    public async Task<ActionResult> Delete(int id)
    {
        await Mediator.Send(new DeleteGenreCommand(id));

        return NoContent();
    }
}
