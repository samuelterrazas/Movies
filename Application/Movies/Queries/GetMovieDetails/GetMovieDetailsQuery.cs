namespace Movies.Application.Movies.Queries.GetMovieDetails;

public record GetMovieDetailsQuery(int Id) : IRequest<MovieDetailsDto>;


public class GetMovieDetailsQueryHandler : IRequestHandler<GetMovieDetailsQuery, MovieDetailsDto>
{
    private readonly IApplicationDbContext _dbContext;

    
    public GetMovieDetailsQueryHandler(IApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    
    public async Task<MovieDetailsDto> Handle(GetMovieDetailsQuery request, CancellationToken cancellationToken)
    {
        var movie = await _dbContext.Movies
            .AsNoTracking()
            .Include(movie => movie.Images)
            .Include(movie => movie.MovieGenres)!.ThenInclude(movieGenre => movieGenre.Genre)
            .Include(movie => movie.MoviePersons)!.ThenInclude(moviePerson => moviePerson.Person)
            .FirstOrDefaultAsync(i => i.Id == request.Id, cancellationToken);

        if (movie is null)
            throw new NotFoundException(nameof(Movie), request.Id);

        return (MovieDetailsDto)movie;
    }
}
