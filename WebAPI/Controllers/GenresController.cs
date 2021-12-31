using System.Net;
using Microsoft.AspNetCore.Mvc;
using Movies.Application.Genres.Commands.CreateGenre;
using Movies.Application.Genres.Commands.DeleteGenre;
using Movies.Application.Genres.Commands.UpdateGenre;
using Movies.Application.Common.DTOs;
using Movies.Application.Genres.Queries.GetGenres;
using Movies.Application.Common.Wrappers;
using NSwag.Annotations;

namespace Movies.WebAPI.Controllers;

[Route("api/genres")]
public class GenresController : ApiControllerBase
{
    #region Documentation
    [SwaggerResponse((int)HttpStatusCode.OK, typeof(PaginatedResponse<GenreDto>))]
    [SwaggerResponse((int)HttpStatusCode.Unauthorized, typeof(ProblemDetails))]
    [SwaggerResponse((int)HttpStatusCode.InternalServerError, typeof(ProblemDetails))]
    #endregion
    [HttpGet]
    //[Authorize(Roles = "Administrator, User")]
    public async Task<ActionResult<PaginatedResponse<GenreDto>>> GetAll([FromQuery] GetGenresQuery query)
    {
        return await Mediator.Send(query);
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
    public async Task<ActionResult<int>> Create([FromBody] CreateGenreCommand command)
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
    public async Task<ActionResult> Update(int id, [FromBody] UpdateGenreCommand command)
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
        await Mediator.Send(new DeleteGenreCommand { Id = id });

        return NoContent();
    }
}
