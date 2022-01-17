using File = Movies.Domain.Entities.File;

namespace Movies.Application.Files.Commands.DeleteFile;

public record DeleteFileCommand(int Id) : IRequest;

public class DeleteFileCommandHandler : IRequestHandler<DeleteFileCommand>
{
    private readonly IApplicationDbContext _dbContext;
    private readonly IFileStore _fileStore;

    public DeleteFileCommandHandler(IApplicationDbContext dbContext, IFileStore fileStore)
    {
        _dbContext = dbContext;
        _fileStore = fileStore;
    }
    
    public async Task<Unit> Handle(DeleteFileCommand request, CancellationToken cancellationToken)
    {
        var file = await _dbContext.Files.FirstOrDefaultAsync(file => file.Id == request.Id, cancellationToken);

        if (file is null)
            throw new NotFoundException(nameof(File), request.Id);

        _dbContext.Files.Remove(file);
        await _fileStore.DeleteFile(file.Url, Enum.GetName(Container.Movies)?.ToLower());
        await _dbContext.SaveChangesAsync(cancellationToken);
        
        return Unit.Value;
    }
}