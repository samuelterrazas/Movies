namespace Movies.Application.Movies.Commands.DeleteMovie;

public record DeleteMovieCommand(int Id) : IRequest;


public class DeleteMovieCommandHandler : IRequestHandler<DeleteMovieCommand>
{
    private readonly IApplicationDbContext _dbContext;

    
    public DeleteMovieCommandHandler(IApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    
    public async Task<Unit> Handle(DeleteMovieCommand request, CancellationToken cancellationToken)
    {
        var movie = await _dbContext.Movies.FirstOrDefaultAsync(movie => movie.Id == request.Id, cancellationToken);

        if (movie is null)
            throw new NotFoundException(nameof(Movie), request.Id);

        _dbContext.Movies.Remove(movie);
        await _dbContext.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}