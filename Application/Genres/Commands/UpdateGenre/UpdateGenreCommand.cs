namespace Movies.Application.Genres.Commands.UpdateGenre;

public record UpdateGenreCommand(int Id, string Name) : IRequest;


public class UpdateGenreCommandHandler : IRequestHandler<UpdateGenreCommand>
{
    private readonly IApplicationDbContext _dbContext;

    
    public UpdateGenreCommandHandler(IApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    
    public async Task<Unit> Handle(UpdateGenreCommand request, CancellationToken cancellationToken)
    {
        var genre = await _dbContext.Genres.FirstOrDefaultAsync(g => g.Id == request.Id, cancellationToken);

        if (genre is null)
            throw new NotFoundException(nameof(Genre), request.Id);
        
        genre.Name = request.Name;
        
        await _dbContext.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}
