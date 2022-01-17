using Microsoft.AspNetCore.Mvc;
using Movies.Application.Files.Commands.DeleteFile;
using Movies.Application.Files.Commands.UpdateFile;
using Movies.Application.Files.Commands.UploadFile;

namespace Movies.WebAPI.Controllers;

[Route("api/files")]
public class FilesController : ApiControllerBase
{
    [HttpPost]
    public async Task<ActionResult<object>> Upload([FromForm] UploadFileCommand command)
    {
        return await Mediator.Send(command);
    }


    [HttpPut("{id}")]
    public async Task<ActionResult> Update(int id, [FromForm] UpdateFileCommand command)
    {
        await Mediator.Send(command);

        return NoContent();
    }


    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(int id)
    {
        await Mediator.Send(new DeleteFileCommand(id));

        return NoContent();
    }
}