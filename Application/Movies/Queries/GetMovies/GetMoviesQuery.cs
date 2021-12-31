using AutoMapper;
using MediatR;
using Movies.Domain.Enums;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using Movies.Application.Common.Interfaces;
using Movies.Application.Common.Mappings;
using Movies.Application.Movies.DTOs;
using Movies.Application.Common.Wrappers;

namespace Movies.Application.Movies.Queries.GetMovies;

public class GetMoviesQuery : IRequest<PaginatedResponse<MovieDto>>
{
    public int PageNumber { get; set; } = 1;
    public int PageSize { get; set; } = 10;
    public string Title { get; set; }
    public string Genre { get; set; }
    public string Director { get; set; }
    public string Actor { get; set; }
}

public class GetMoviesQueryHandler : IRequestHandler<GetMoviesQuery, PaginatedResponse<MovieDto>>
{
    private readonly IApplicationDbContext _dbContext;
    private readonly IMapper _mapper;

    public GetMoviesQueryHandler(IApplicationDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }
    
    public async Task<PaginatedResponse<MovieDto>> Handle(GetMoviesQuery request, CancellationToken cancellationToken)
    {
        var movies = _dbContext.Movies.AsQueryable();

        if (!string.IsNullOrEmpty(request.Title))
            movies = movies.Where(m => m.Title.Contains(request.Title));

        if (!string.IsNullOrEmpty(request.Genre))
            movies = _dbContext.MovieGenres
                .Where(movieGenre => movieGenre.Genre.Name.Contains(request.Genre))
                .Select(movieGenre => movieGenre.Movie);

        if (!string.IsNullOrEmpty(request.Director))
            movies = _dbContext.MoviePersons
                .Where(moviePerson => moviePerson.Person.FullName.Contains(request.Director))
                .Where(moviePerson => moviePerson.Role.Equals(Role.Director))
                .Select(moviePerson => moviePerson.Movie);

        if (!string.IsNullOrEmpty(request.Actor))
            movies = _dbContext.MoviePersons
                .Where(moviePerson => moviePerson.Person.FullName.Contains(request.Actor))
                .Where(moviePerson => moviePerson.Role.Equals(Role.Cast))
                .Select(moviePerson => moviePerson.Movie);

        return await movies
            .AsNoTracking()
            .OrderByDescending(m => m.Release)
            .ProjectTo<MovieDto>(_mapper.ConfigurationProvider)
            .PaginatedResponseAsync(request.PageNumber, request.PageSize);
    }
}
