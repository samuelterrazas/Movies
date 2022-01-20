using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Movies.Common.DTOs;
using Movies.Application.Movies.Commands.CreateMovie;
using Movies.Application.Movies.Commands.DeleteMovie;
using Movies.Application.Movies.Commands.UpdateMovie;
using Movies.Application.Movies.Queries.GetMovieDetails;
using Movies.Application.Movies.Queries.GetMovies;
using Movies.Common.Wrappers;

namespace Movies.WebAPI.Controllers;

[Route("api/movies")]
public class MoviesController : ApiControllerBase
{
    [HttpGet]
    [Authorize(Roles = "Administrator, User")]
    public async Task<ActionResult<PaginatedResponse<MoviesDto>>> GetAll([FromQuery] GetMoviesQuery query)
    {
        return await Mediator.Send(query);
    }
    
    
    [HttpGet("{id}")]
    [Authorize(Roles = "Administrator, User")]
    public async Task<ActionResult<MovieDetailsDto>> GetDetails(int id)
    {
        return await Mediator.Send(new GetMovieDetailsQuery(id));
    }

    
    [HttpPost]
    [Authorize(Roles = "Administrator")]
    public async Task<ActionResult<int>> Create([FromBody] CreateMovieCommand command)
    {
        return await Mediator.Send(command);
    }

    
    [HttpPut("{id}")]
    [Authorize(Roles = "Administrator")]
    public async Task<ActionResult> Update(int id, [FromBody] UpdateMovieCommand command)
    {
        await Mediator.Send(command);

        return NoContent();
    }

    
    [HttpDelete("{id}")]
    [Authorize(Roles = "Administrator")]
    public async Task<ActionResult> Delete(int id)
    {
        await Mediator.Send(new DeleteMovieCommand(id));

        return NoContent();
    }
}
