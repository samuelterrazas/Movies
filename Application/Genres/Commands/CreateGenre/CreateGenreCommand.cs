namespace Movies.Application.Genres.Commands.CreateGenre;

public record CreateGenreCommand(string Name) : IRequest<int>;


public class CreateGenreCommandHandler : IRequestHandler<CreateGenreCommand, int>
{
    private readonly IApplicationDbContext _dbContext;

    
    public CreateGenreCommandHandler(IApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    
    public async Task<int> Handle(CreateGenreCommand request, CancellationToken cancellationToken)
    {
        var genre = new Genre {Name = request.Name};

        _dbContext.Genres.Add(genre);
        await _dbContext.SaveChangesAsync(cancellationToken);

        return genre.Id;
    }
}
