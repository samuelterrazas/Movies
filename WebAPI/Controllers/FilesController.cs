using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Movies.Application.Files.Commands.DeleteFile;
using Movies.Application.Files.Commands.UpdateFile;
using Movies.Application.Files.Commands.UploadFile;

namespace Movies.WebAPI.Controllers;

[Route("api/files")]
public class FilesController : ApiControllerBase
{
    [HttpPost]
    //[Authorize(Roles = "Administrator")]
    public async Task<ActionResult<object>> Upload([FromForm] UploadFileCommand command)
    {
        return await Mediator.Send(command);
    }

    
    [HttpPut("{id}")]
    //[Authorize(Roles = "Administrator")]
    public async Task<ActionResult> Update(int id, [FromForm] UpdateFileCommand command)
    {
        command = command with {Id = id};
        await Mediator.Send(command);

        return NoContent();
    }

    
    [HttpDelete("{id}")]
    //[Authorize(Roles = "Administrator")]
    public async Task<ActionResult> Delete(int id)
    {
        await Mediator.Send(new DeleteFileCommand(id));

        return NoContent();
    }
}