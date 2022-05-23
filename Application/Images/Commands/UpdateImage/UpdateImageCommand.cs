namespace Movies.Application.Images.Commands.UpdateImage;

public record UpdateImageCommand(int Id, int MovieId, IFormFile Image) : IRequest;


public class UpdateImageCommandHandler : IRequestHandler<UpdateImageCommand>
{
    private readonly IApplicationDbContext _dbContext;
    private readonly IFileStore _fileStore;

    
    public UpdateImageCommandHandler(IApplicationDbContext dbContext, IFileStore fileStore)
    {
        _dbContext = dbContext;
        _fileStore = fileStore;
    }
    
    
    public async Task<Unit> Handle(UpdateImageCommand request, CancellationToken cancellationToken)
    {
        var image = await _dbContext.Images.FirstOrDefaultAsync(image => image.Id == request.Id, cancellationToken);
        
        if (image is null)
            throw new NotFoundException(nameof(Image), request.Id);
        
        var movie = await _dbContext.Movies.FirstOrDefaultAsync(movie => movie.Id == request.MovieId, cancellationToken);
        
        if (movie is null)
            throw new NotFoundException(nameof(Movie), request.MovieId);

        string url;
        
        await using (var memoryStream = new MemoryStream())
        {
            await request.Image.CopyToAsync(memoryStream, cancellationToken);

            var content = memoryStream.ToArray();
            var extension = Path.GetExtension(request.Image.FileName);
        
            url = await _fileStore.EditFile(content, extension, Container.Movies.GetDescription(), image.Url, request.Image.ContentType);
        }

        image.MovieId = request.MovieId;
        image.Url = url;
        
        await _dbContext.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}