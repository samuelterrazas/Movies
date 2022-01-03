namespace Movies.Application.Movies.Commands.DeleteMovie;

public record DeleteMovieCommand(int Id) : IRequest;

public class DeleteMovieCommandHandler : IRequestHandler<DeleteMovieCommand>
{
    private readonly IApplicationDbContext _dbContext;
    private readonly IFileStore _fileStore;
    private const string Container = "movies";

    public DeleteMovieCommandHandler(IApplicationDbContext dbContext, IFileStore fileStore)
    {
        _dbContext = dbContext;
        _fileStore = fileStore;
    }
        
    public async Task<Unit> Handle(DeleteMovieCommand request, CancellationToken cancellationToken)
    {
        var movie = await _dbContext.Movies.FirstOrDefaultAsync(movie => movie.Id == request.Id, cancellationToken);

        if (movie is null)
            throw new NotFoundException(nameof(Movie), request.Id);

        _dbContext.Movies.Remove(movie);
        await _fileStore.DeleteFile(movie.Image, Container);
        await _dbContext.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}