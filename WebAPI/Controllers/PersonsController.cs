﻿using System.Net;
using Microsoft.AspNetCore.Mvc;
using Movies.Application.Common.Models;
using Movies.Application.Persons.Commands.CreatePerson;
using Movies.Application.Persons.Commands.DeletePerson;
using Movies.Application.Persons.Commands.UpdatePerson;
using Movies.Application.Persons.DTOs;
using Movies.Application.Persons.Queries.GetPersons;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Movies.Application.Persons.Queries.GetPersonDetails;
using NSwag.Annotations;

namespace Movies.WebAPI.Controllers
{
    [Route("api/persons")]
    public class PersonsController : ApiControllerBase
    {
        #region Documentation
        [SwaggerResponse((int)HttpStatusCode.OK, typeof(PaginatedResponse<PersonDto>))]
        [SwaggerResponse((int)HttpStatusCode.Unauthorized, typeof(ProblemDetails))]
        [SwaggerResponse((int)HttpStatusCode.InternalServerError, typeof(ProblemDetails))]
        #endregion
        [HttpGet]
        [Authorize(Roles = "Administrator, User")]
        public async Task<ActionResult<PaginatedResponse<PersonDto>>> GetAll([FromQuery] GetPersonsQuery query)
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
        [Authorize(Roles = "Administrator")]
        public async Task<ActionResult<PersonDetailsDto>> GetDetails(int id)
        {
            return await Mediator.Send(new GetPersonDetailsQuery {Id = id});
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
        [Authorize(Roles = "Administrator")]
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
        [Authorize(Roles = "Administrator")]
        public async Task<ActionResult> Delete(int id)
        {
            await Mediator.Send(new DeletePersonCommand { Id = id });

            return NoContent();
        }
    }
}