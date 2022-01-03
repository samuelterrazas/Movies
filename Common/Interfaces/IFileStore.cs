namespace Movies.Common.Interfaces;

public interface IFileStore
{
    Task<string> SaveFile(byte[] content, string extension, string container, string contentType);

    Task<string> EditFile(byte[] content, string extension, string container, string path, string contentType);

    Task DeleteFile(string path, string container);
}
