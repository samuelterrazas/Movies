using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using Microsoft.Extensions.Configuration;
using Movies.Common.Interfaces;

namespace Movies.Infrastructure.Files.Azure;

public class AzureFileStore : IFileStore
{
    private readonly string _connectionStr;
        
    public AzureFileStore(IConfiguration configuration)
    {
        _connectionStr = configuration.GetConnectionString("AzureStorage");
    }
        
    public async Task<string> SaveFile(byte[] content, string extension, string container, string contentType)
    {
        var client = new BlobContainerClient(_connectionStr, container);
        await client.CreateIfNotExistsAsync();
        await client.SetAccessPolicyAsync(PublicAccessType.Blob);

        var fileName = $"{Guid.NewGuid()}{extension}";
        var blob = client.GetBlobClient(fileName);
            
        var blobHttpHeaders = new BlobHttpHeaders {ContentType = contentType};
        var blobUploadOptions = new BlobUploadOptions {HttpHeaders = blobHttpHeaders};

        await blob.UploadAsync(new BinaryData(content), blobUploadOptions);

        return blob.Uri.ToString();
    }

    public async Task<string> EditFile(byte[] content, string extension, string container, string path, string contentType)
    {
        await DeleteFile(path, container);

        return await SaveFile(content, extension, container, contentType);
    }

    public async Task DeleteFile(string path, string container)
    {
        if (string.IsNullOrEmpty(path))
            return;

        var client = new BlobContainerClient(_connectionStr, container);
        await client.CreateIfNotExistsAsync();

        var file = Path.GetFileName(path);
        var blob = client.GetBlobClient(file);

        await blob.DeleteIfExistsAsync();
    }
}