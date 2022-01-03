namespace Movies.Application.Genres.Commands.DeleteGenre;

public record DeleteGenreCommand(int Id) : IRequest;

public class DeleteGenreCommandHandler : IRequestHandler<DeleteGenreCommand>
{
    private readonly IApplicationDbContext _dbContext;

    public DeleteGenreCommandHandler(IApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }
        
    public async Task<Unit> Handle(DeleteGenreCommand request, CancellationToken cancellationToken)
    {
        var genre = await _dbContext.Genres.FirstOrDefaultAsync(g => g.Id == request.Id, cancellationToken);

        if (genre is null)
            throw new NotFoundException(nameof(Genre), request.Id);

        _dbContext.Genres.Remove(genre);
        await _dbContext.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}
