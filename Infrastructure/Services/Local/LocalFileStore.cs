namespace Movies.Infrastructure.Services.Local;

public class LocalFileStore : IFileStore
{
    private readonly IWebHostEnvironment _environment;
    private readonly IHttpContextAccessor _contextAccessor;

    
    public LocalFileStore(IWebHostEnvironment environment, IHttpContextAccessor contextAccessor)
    {
        _environment = environment;
        _contextAccessor = contextAccessor;
    }
    
    
    public async Task<string> SaveFile(byte[] content, string extension, string container, string contentType)
    {
        var fileName = $"{Guid.NewGuid()}{extension}";

        var folder = Path.Combine(_environment.WebRootPath, container);

        if (!Directory.Exists(folder))
            Directory.CreateDirectory(folder);

        var path = Path.Combine(folder, fileName);
        await File.WriteAllBytesAsync(path, content);

        var url = $"{_contextAccessor.HttpContext!.Request.Scheme}://{_contextAccessor.HttpContext.Request.Host}";

        var currentUrl = Path.Combine(url, container, fileName).Replace("\\", "/");

        return currentUrl;
    }

    public async Task<string> EditFile(byte[] content, string extension, string container, string path, string contentType)
    {
        await DeleteFile(path, container);

        return await SaveFile(content, extension, container, contentType);
    }

    public Task DeleteFile(string path, string container)
    {
        if (string.IsNullOrEmpty(path))
            return Task.FromResult(0);

        var fileName = Path.GetFileName(path);
        var fileDirectory = Path.Combine(_environment.WebRootPath, container, fileName);
        
        if(File.Exists(fileDirectory))
            File.Delete(fileDirectory);

        return Task.FromResult(0);
    }
}