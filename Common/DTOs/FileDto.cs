using Movies.Domain.Entities;

namespace Movies.Common.DTOs;

public record FilesDto(int Id, string Url)
{
    public static explicit operator FilesDto(Image image) => 
        new(
            image.Id, 
            image.Url
        );
};