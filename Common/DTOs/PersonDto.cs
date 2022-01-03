using Movies.Domain.Entities;

namespace Movies.Common.DTOs;

public record PersonsDto(int Id, string FullName)
{
    public static explicit operator PersonsDto(Person person) => new(person.Id, person.FullName);
}

public record PersonDetailsDto(int Id, string FullName, List<MoviesDto> Movies)
{
    public static explicit operator PersonDetailsDto(Person person)
        => new
        (
            person.Id,
            person.FullName,
            person.MoviePersons
                .Select(moviePerson => new MoviesDto(moviePerson.MovieId, moviePerson.Movie.Title, moviePerson.Movie.Image))
                .ToList()
        );
}