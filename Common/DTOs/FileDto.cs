using File = Movies.Domain.Entities.File;

namespace Movies.Common.DTOs;

public record FilesDto(int Id, string Url)
{
    public static explicit operator FilesDto(File file) => new FilesDto(file.Id, file.Url);
};