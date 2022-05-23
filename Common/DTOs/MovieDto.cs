namespace Movies.Common.DTOs;

public record MoviesDto(int Id, string Title, ICollection<FilesDto>? Images)
{
    public static explicit operator MoviesDto(Movie movie)
    {
        return new MoviesDto(
            Id: movie.Id, 
            Title: movie.Title,
            Images: movie.Images?
                .Select(file => new FilesDto(Id: file.Id, Url: file.Url))
                .ToList()
        );
    }
}


public record MovieDetailsDto(
    int Id, 
    string Title, 
    int Release, 
    string Duration, 
    string MaturityRating, 
    string Summary,
    string Teaser,
    ICollection<FilesDto>? Images,
    ICollection<GenresDto>? Genres, 
    ICollection<PersonsDto>? DirectedBy, 
    ICollection<PersonsDto>? Cast
)
{
    public static explicit operator MovieDetailsDto(Movie movie)
    {
        return new MovieDetailsDto(
            Id: movie.Id,
            Title: movie.Title,
            Release: movie.Release,
            Duration: movie.Duration,
            MaturityRating: movie.MaturityRating,
            Summary: movie.Summary,
            Teaser: movie.Teaser,
            Images: movie.Images?
                .Select(file => new FilesDto(Id: file.Id, Url: file.Url))
                .ToList(),
            Genres: movie.MovieGenres?
                .Select(movieGenre => new GenresDto(Id: movieGenre.GenreId, Name: movieGenre.Genre.Name))
                .ToList(),
            DirectedBy: movie.MoviePersons?
                .Where(moviePerson => moviePerson.Role == (byte)Role.Director)
                .OrderBy(moviePerson => moviePerson.Order)
                .Select(moviePerson => new PersonsDto(Id: moviePerson.PersonId, FullName: moviePerson.Person.FullName))
                .ToList(),
            Cast: movie.MoviePersons?
                .Where(moviePerson => moviePerson.Role == (byte)Role.Cast)
                .OrderBy(moviePerson => moviePerson.Order)
                .Select(moviePerson => new PersonsDto(Id: moviePerson.PersonId, FullName: moviePerson.Person.FullName))
                .ToList()
        );
    }
}


public record MoviePersonDto(int PersonId, byte Role, byte Order);