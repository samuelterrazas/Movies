using Microsoft.AspNetCore.Http;

namespace Movies.Application.Images.Commands.UploadImage;

public record UploadImageCommand(int MovieId, IFormFile Image) : IRequest<object>;

public class UploadImageCommandHandler : IRequestHandler<UploadImageCommand, object>
{
    private readonly IApplicationDbContext _dbContext;
    private readonly IFileStore _fileStore;

    public UploadImageCommandHandler(IApplicationDbContext dbContext, IFileStore fileStore)
    {
        _dbContext = dbContext;
        _fileStore = fileStore;
    }
    
    public async Task<object> Handle(UploadImageCommand request, CancellationToken cancellationToken)
    {
        var movie = await _dbContext.Movies.FirstOrDefaultAsync(movie => movie.Id == request.MovieId, cancellationToken);

        if (movie is null)
            throw new NotFoundException(nameof(Movie), request.MovieId);

        await using var memoryStream = new MemoryStream();
        await request.Image.CopyToAsync(memoryStream, cancellationToken);

        var content = memoryStream.ToArray();
        var extension = Path.GetExtension(request.Image.FileName);
        
        var url = await _fileStore.SaveFile(content, extension, Enum.GetName(Container.Movies)?.ToLower(), 
            request.Image.ContentType);

        var image = new Image {MovieId = request.MovieId, Url = url};

        _dbContext.Images.Add(image);
        await _dbContext.SaveChangesAsync(cancellationToken);

        return new {MovieId = movie.Id, Image = image.Url};
    }
}