﻿namespace Movies.Application.Movies.Commands.CreateMovie;

public record CreateMovieCommand(
    string Title,
    short Release,
    string Duration,
    string MaturityRating,
    string Summary,
    string Teaser,
    ICollection<int> Genres,
    ICollection<MoviePersonDto> Persons
) : IRequest<int>;


public class CreateMovieCommandHandler : IRequestHandler<CreateMovieCommand, int>
{
    private readonly IApplicationDbContext _dbContext;

    
    public CreateMovieCommandHandler(IApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    
    public async Task<int> Handle(CreateMovieCommand request, CancellationToken cancellationToken)
    {
        var movie = new Movie
        {
            Title = request.Title,
            Release = request.Release,
            Duration = request.Duration,
            MaturityRating = request.MaturityRating,
            Summary = request.Summary,
            Teaser = $"{Links.YouTube}{request.Teaser}",
            MovieGenres = request.Genres
                .Select(genreId => new MovieGenre {GenreId = genreId})
                .ToList(),
            MoviePersons = request.Persons
                .Select(moviePersonDto => new MoviePerson
                {
                    PersonId = moviePersonDto.PersonId, 
                    Role = moviePersonDto.Role,
                    Order = moviePersonDto.Order
                })
                .ToList()
        };

        _dbContext.Movies.Add(movie);
        await _dbContext.SaveChangesAsync(cancellationToken);
            
        return movie.Id;
    }
}
