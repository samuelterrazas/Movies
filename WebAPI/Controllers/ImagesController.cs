namespace Movies.WebAPI.Controllers;

[Route("api/files")]
public class ImagesController : ApiControllerBase
{
    [HttpPost]
    [Authorize(Roles = "Administrator")]
    public async Task<IActionResult> Upload([FromForm] UploadImageCommand command) => Ok(await Mediator.Send(command));


    [HttpPut("{id:int}")]
    [Authorize(Roles = "Administrator")]
    public async Task<IActionResult> Update(int id, [FromForm] UpdateImageCommand command)
    {
        command = command with {Id = id};
        await Mediator.Send(command);

        return NoContent();
    }

    
    [HttpDelete("{id:int}")]
    [Authorize(Roles = "Administrator")]
    public async Task<IActionResult> Delete(int id)
    {
        await Mediator.Send(new DeleteImageCommand(id));

        return NoContent();
    }
}