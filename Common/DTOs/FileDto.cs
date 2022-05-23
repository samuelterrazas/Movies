namespace Movies.Common.DTOs;

public record FilesDto(int Id, string Url)
{
    public static explicit operator FilesDto(Image image)
    {
        return new FilesDto(
            Id: image.Id, 
            Url: image.Url
        );
    }
};