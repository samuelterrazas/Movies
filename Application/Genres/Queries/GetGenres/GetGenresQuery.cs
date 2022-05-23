namespace Movies.Application.Genres.Queries.GetGenres;

public record GetGenresQuery() : IRequest<IEnumerable<GenresDto>>;


public class GetGenresQueryHandler : IRequestHandler<GetGenresQuery, IEnumerable<GenresDto>>
{
    private readonly IApplicationDbContext _dbContext;

    
    public GetGenresQueryHandler(IApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    
    public async Task<IEnumerable<GenresDto>> Handle(GetGenresQuery request, CancellationToken cancellationToken)
    {
        return await _dbContext.Genres
            .AsNoTracking()
            .Select(genre => (GenresDto)genre)
            .ToListAsync(cancellationToken);
    }
}
