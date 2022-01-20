using Microsoft.AspNetCore.Http;
using File = Movies.Domain.Entities.File;

namespace Movies.Application.Files.Commands.UpdateFile;

public record UpdateFileCommand(int Id, int MovieId, IFormFile Image) : IRequest;

public class UpdateFileCommandHandler : IRequestHandler<UpdateFileCommand>
{
    private readonly IApplicationDbContext _dbContext;
    private readonly IFileStore _fileStore;

    public UpdateFileCommandHandler(IApplicationDbContext dbContext, IFileStore fileStore)
    {
        _dbContext = dbContext;
        _fileStore = fileStore;
    }
    
    public async Task<Unit> Handle(UpdateFileCommand request, CancellationToken cancellationToken)
    {
        var movie = await _dbContext.Movies.FirstOrDefaultAsync(movie => movie.Id == request.MovieId, cancellationToken);
        
        if (movie is null)
            throw new NotFoundException(nameof(Movie), request.MovieId);
        
        var file = await _dbContext.Files.FirstOrDefaultAsync(file => file.Id == request.Id, cancellationToken);
        
        if (file is null)
            throw new NotFoundException(nameof(File), request.Id);

        await using var memoryStream = new MemoryStream();
        await request.Image.CopyToAsync(memoryStream, cancellationToken);

        var content = memoryStream.ToArray();
        var extension = Path.GetExtension(request.Image.FileName);
        
        var image = await _fileStore.EditFile(content, extension, Enum.GetName(Container.Movies)?.ToLower(), file.Url, 
            request.Image.ContentType);

        file.MovieId = request.MovieId;
        file.Url = image;
        
        await _dbContext.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}