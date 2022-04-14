namespace Movies.Application.Movies.Commands.UpdateMovie;

public record UpdateMovieCommand(
    int Id,
    string Title,
    int Release,
    string Duration,
    string MaturityRating,
    string Summary,
    List<int> Genres,
    List<MoviePersonDto> Persons
) : IRequest;

public class UpdateMovieCommandHandler : IRequestHandler<UpdateMovieCommand>
{
    private readonly IApplicationDbContext _dbContext;

    public UpdateMovieCommandHandler(IApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Unit> Handle(UpdateMovieCommand request, CancellationToken cancellationToken)
    {
        var movie = await _dbContext.Movies
            .Include(i => i.MovieGenres)
            .Include(i => i.MoviePersons)
            .FirstOrDefaultAsync(i => i.Id == request.Id, cancellationToken);

        if (movie is null)
            throw new NotFoundException(nameof(Movie), request.Id);

        movie.Title = request.Title;
        movie.Release = request.Release;
        movie.Duration = request.Duration;
        movie.MaturityRating = request.MaturityRating;
        movie.Summary = request.Summary;
        movie.MovieGenres = request.Genres
            .Select(genreId => new MovieGenre {GenreId = genreId})
            .ToList();
        movie.MoviePersons = request.Persons
            .Select(moviePersonDto => new MoviePerson
            {
                PersonId = moviePersonDto.PersonId,
                Role = moviePersonDto.Role,
                Order = moviePersonDto.Order
            })
            .ToList();

        await _dbContext.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}
