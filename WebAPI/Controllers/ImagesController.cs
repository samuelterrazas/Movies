using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Movies.Application.Images.Commands.DeleteImage;
using Movies.Application.Images.Commands.UpdateImage;
using Movies.Application.Images.Commands.UploadImage;

namespace Movies.WebAPI.Controllers;

[Route("api/files")]
public class ImagesController : ApiControllerBase
{
    [HttpPost]
    [Authorize(Roles = "Administrator")]
    public async Task<ActionResult<object>> Upload([FromForm] UploadImageCommand command)
    {
        return await Mediator.Send(command);
    }

    
    [HttpPut("{id}")]
    [Authorize(Roles = "Administrator")]
    public async Task<ActionResult> Update(int id, [FromForm] UpdateImageCommand command)
    {
        command = command with {Id = id};
        await Mediator.Send(command);

        return NoContent();
    }

    
    [HttpDelete("{id}")]
    [Authorize(Roles = "Administrator")]
    public async Task<ActionResult> Delete(int id)
    {
        await Mediator.Send(new DeleteImageCommand(id));

        return NoContent();
    }
}