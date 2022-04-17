namespace Movies.Common.DTOs;

public record GenresDto(int Id, string Name)
{
    public static explicit operator GenresDto(Genre genre) => 
        new(
            genre.Id,
            genre.Name
        );
}