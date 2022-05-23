namespace Movies.Common.DTOs;

public record PersonsDto(int Id, string FullName)
{
    public static explicit operator PersonsDto(Person person)
    {
        return new PersonsDto(
            Id: person.Id,
            FullName: person.FullName
        );
    }
}


public record PersonDetailsDto(int Id, string FullName, ICollection<MoviesDto>? Movies)
{
    public static explicit operator PersonDetailsDto(Person person)
    {
        return new PersonDetailsDto(
            Id: person.Id,
            FullName: person.FullName,
            Movies: person.MoviePersons?
                .Select(moviePerson => 
                    new MoviesDto(
                        Id: moviePerson.MovieId, 
                        Title: moviePerson.Movie.Title,
                        Images: moviePerson.Movie.Images?
                            .Select(file => new FilesDto(Id: file.Id, Url: file.Url))
                            .ToList()
                    )
                )
                .ToList()
        );
    }
}