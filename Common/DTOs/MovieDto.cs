using Movies.Domain.Entities;
using Movies.Domain.Enums;

namespace Movies.Common.DTOs;

public record MoviesDto(int Id, string Title, string Image)
{
    public static explicit operator MoviesDto(Movie movie) => new(movie.Id, movie.Title, movie.Image);
}

public record MovieDetailsDto
(
    int Id,
    string Title,
    int Release,
    string Duration,
    string MaturityRating,
    string Summary,
    string Image,
    List<GenresDto> Genres,
    List<PersonsDto> DirectedBy,
    List<PersonsDto> Cast
)
{
    public static explicit operator MovieDetailsDto(Movie movie)
        => new
        (
            movie.Id,
            movie.Title,
            movie.Release,
            movie.Duration,
            movie.MaturityRating,
            movie.Summary,
            movie.Image,
            movie.MovieGenres
                .Select(movieGenre => new GenresDto(movieGenre.GenreId, movieGenre.Genre.Name))
                .ToList(),
            movie.MoviePersons
                .Where(moviePerson => moviePerson.Role == Role.Director)
                .OrderBy(moviePerson => moviePerson.Order)
                .Select(moviePerson => new PersonsDto(moviePerson.PersonId, moviePerson.Person.FullName))
                .ToList(),
            movie.MoviePersons
                .Where(moviePerson => moviePerson.Role == Role.Cast)
                .OrderBy(moviePerson => moviePerson.Order)
                .Select(moviePerson => new PersonsDto(moviePerson.PersonId, moviePerson.Person.FullName))
                .ToList()
        );
}

public record MoviePersonDto(int PersonId, int Role, int Order);