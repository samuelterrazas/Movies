namespace Movies.Common.DTOs;

public record GenresDto(int Id, string Name)
{
    public static explicit operator GenresDto(Genre genre)
    {
        return new GenresDto(
            Id: genre.Id,
            Name: genre.Name
        );
    }
}