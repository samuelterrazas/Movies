namespace Movies.Application.Genres.Queries.GetGenres;

public record GetGenresQuery() : IRequest<List<GenresDto>>;

public class GetGenresQueryHandler : IRequestHandler<GetGenresQuery, List<GenresDto>>
{
    private readonly IApplicationDbContext _dbContext;

    public GetGenresQueryHandler(IApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }
        
    public async Task<List<GenresDto>> Handle(GetGenresQuery request, CancellationToken cancellationToken)
    {
        return await _dbContext.Genres
            .AsNoTracking()
            .Select(genre => (GenresDto)genre)
            .ToListAsync(cancellationToken);
    }
}
