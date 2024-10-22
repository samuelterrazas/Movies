﻿namespace Movies.Application.Movies.Queries.GetMovies;

public record GetMoviesQuery(
    short? PageNumber,
    short? PageSize,
    string? Title,
    string? Genre,
    string? Director,
    string? Actor
) : IRequest<PaginatedResponse<MoviesDto>>;


public class GetMoviesQueryHandler : IRequestHandler<GetMoviesQuery, PaginatedResponse<MoviesDto>>
{
    private readonly IApplicationDbContext _dbContext;

    
    public GetMoviesQueryHandler(IApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    
    public async Task<PaginatedResponse<MoviesDto>> Handle(GetMoviesQuery request, CancellationToken cancellationToken)
    {
        var movies = _dbContext.Movies
            .AsQueryable()
            .AsNoTracking();

        if (!string.IsNullOrEmpty(request.Title))
            movies = movies.Where(m => m.Title.Contains(request.Title));

        if (!string.IsNullOrEmpty(request.Genre))
            movies = _dbContext.MovieGenres
                .Where(movieGenre => movieGenre.Genre.Name.Contains(request.Genre))
                .Select(movieGenre => movieGenre.Movie);

        if (!string.IsNullOrEmpty(request.Director))
            movies = _dbContext.MoviePersons
                .Where(moviePerson => moviePerson.Person.FullName.Contains(request.Director))
                .Where(moviePerson => moviePerson.Role == (byte)Role.Director)
                .Select(moviePerson => moviePerson.Movie);

        if (!string.IsNullOrEmpty(request.Actor))
            movies = _dbContext.MoviePersons
                .Where(moviePerson => moviePerson.Person.FullName.Contains(request.Actor))
                .Where(moviePerson => moviePerson.Role == (byte)Role.Cast)
                .Select(moviePerson => moviePerson.Movie);

        return await movies
            .Include(movie => movie.Images)
            .OrderByDescending(movie => movie.Release)
            .Select(movie => (MoviesDto)movie)
            .PaginatedResponseAsync(request.PageNumber ?? 1, request.PageSize ?? 10);
    }
}
