using Microsoft.AspNetCore.Http;
using File = Movies.Domain.Entities.File;

namespace Movies.Application.Files.Commands.UploadFile;

public record UploadFileCommand(int MovieId, IFormFile Image) : IRequest<object>;

public class UploadFileCommandHandler : IRequestHandler<UploadFileCommand, object>
{
    private readonly IApplicationDbContext _dbContext;
    private readonly IFileStore _fileStore;

    public UploadFileCommandHandler(IApplicationDbContext dbContext, IFileStore fileStore)
    {
        _dbContext = dbContext;
        _fileStore = fileStore;
    }
    
    public async Task<object> Handle(UploadFileCommand request, CancellationToken cancellationToken)
    {
        var movie = await _dbContext.Movies.FirstOrDefaultAsync(movie => movie.Id == request.MovieId, cancellationToken);

        if (movie is null)
            throw new NotFoundException(nameof(Movie), request.MovieId);

        await using var memoryStream = new MemoryStream();
        await request.Image.CopyToAsync(memoryStream, cancellationToken);

        var content = memoryStream.ToArray();
        var extension = Path.GetExtension(request.Image.FileName);
        
        var image = await _fileStore.SaveFile(content, extension, Enum.GetName(Container.Movies)?.ToLower(), 
            request.Image.ContentType);

        var file = new File {MovieId = request.MovieId, Url = image};

        _dbContext.Files.Add(file);
        await _dbContext.SaveChangesAsync(cancellationToken);

        return new {MovieId = movie.Id, Image = file.Url};
    }
}