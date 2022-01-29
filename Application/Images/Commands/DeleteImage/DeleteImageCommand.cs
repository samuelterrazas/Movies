namespace Movies.Application.Images.Commands.DeleteImage;

public record DeleteImageCommand(int Id) : IRequest;

public class DeleteImageCommandHandler : IRequestHandler<DeleteImageCommand>
{
    private readonly IApplicationDbContext _dbContext;
    private readonly IFileStore _fileStore;

    public DeleteImageCommandHandler(IApplicationDbContext dbContext, IFileStore fileStore)
    {
        _dbContext = dbContext;
        _fileStore = fileStore;
    }
    
    public async Task<Unit> Handle(DeleteImageCommand request, CancellationToken cancellationToken)
    {
        var image = await _dbContext.Images.FirstOrDefaultAsync(image => image.Id == request.Id, cancellationToken);

        if (image is null)
            throw new NotFoundException(nameof(Image), request.Id);

        _dbContext.Images.Remove(image);
        await _fileStore.DeleteFile(image.Url, Enum.GetName(Container.Movies)!.ToLower());
        await _dbContext.SaveChangesAsync(cancellationToken);
        
        return Unit.Value;
    }
}