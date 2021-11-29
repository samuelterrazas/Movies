using System.Net;
using Microsoft.AspNetCore.Mvc;
using Movies.Application.Common.Models;
using Movies.Application.Movies.Commands.CreateMovie;
using Movies.Application.Movies.Commands.DeleteMovie;
using Movies.Application.Movies.Commands.UpdateMovie;
using Movies.Application.Movies.DTOs;
using Movies.Application.Movies.Queries.GetMovieDetails;
using Movies.Application.Movies.Queries.GetMovies;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using NSwag.Annotations;

namespace Movies.WebAPI.Controllers
{
    [Route("api/movies")]
    public class MoviesController : ApiControllerBase
    {
        #region Documentation
        [SwaggerResponse((int)HttpStatusCode.OK, typeof(PaginatedResponse<MovieDto>))]
        [SwaggerResponse((int)HttpStatusCode.Unauthorized, typeof(ProblemDetails))]
        [SwaggerResponse((int)HttpStatusCode.InternalServerError, typeof(ProblemDetails))]
        #endregion
        [HttpGet]
        [Authorize(Roles = "Administrator, User")]
        public async Task<ActionResult<PaginatedResponse<MovieDto>>> GetAll([FromQuery] GetMoviesQuery query)
        {
            return await Mediator.Send(query);
        }
        
        
        #region Documentation
        [SwaggerResponse((int)HttpStatusCode.OK, typeof(MovieDetailsDto))]
        [SwaggerResponse((int)HttpStatusCode.NotFound, typeof(ProblemDetails))]
        [SwaggerResponse((int)HttpStatusCode.Unauthorized, typeof(ProblemDetails))]
        [SwaggerResponse((int)HttpStatusCode.InternalServerError, typeof(ProblemDetails))]
        #endregion
        [HttpGet("{id}")]
        [Authorize(Roles = "Administrator, User")]
        public async Task<ActionResult<MovieDetailsDto>> GetDetails(int id)
        {
            return await Mediator.Send(new GetMovieDetailsQuery { Id = id });
        }


        #region Documentation
        [SwaggerResponse((int)HttpStatusCode.OK, typeof(int))]
        [SwaggerResponse((int)HttpStatusCode.BadRequest, typeof(ProblemDetails))]
        [SwaggerResponse((int)HttpStatusCode.Unauthorized, typeof(ProblemDetails))]
        [SwaggerResponse((int)HttpStatusCode.Forbidden, typeof(ProblemDetails))]
        [SwaggerResponse((int)HttpStatusCode.InternalServerError, typeof(ProblemDetails))]
        #endregion
        [HttpPost]
        [Authorize(Roles = "Administrator")]
        public async Task<ActionResult<int>> Create([FromForm] CreateMovieCommand command)
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
        [Authorize(Roles = "Administrator")]
        public async Task<ActionResult> Update(int id, [FromForm] UpdateMovieCommand command)
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
        [Authorize(Roles = "Administrator")]
        public async Task<ActionResult> Delete(int id)
        {
            await Mediator.Send(new DeleteMovieCommand { Id = id });

            return NoContent();
        }
    }
}
