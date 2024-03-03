using System.Net.Mime;
using Application.Interfaces;
using Azure.Storage;
using Azure.Storage.Blobs;
using Azure.Storage.Sas;
using Domain.Enums;

namespace Infrastructure.Services;

public class AzureBlobStorageService : IStorageService
{
    private readonly BlobServiceClient blobServiceClient;
    private readonly ICurrentUserContextService currentUserContextService;
    private readonly string storageAccountKey;

    public AzureBlobStorageService(
        BlobServiceClient blobServiceClient,
        IConfiguration configuration,
        ICurrentUserContextService currentUserContextService
    )
    {
        this.blobServiceClient = blobServiceClient;

        var connectionString = configuration.GetConnectionString("AzureStorageAccountKey");

        if (string.IsNullOrEmpty(connectionString))
        {
            throw new Exception("Connection string is empty or null");
        }

        this.storageAccountKey = connectionString;
        this.currentUserContextService = currentUserContextService;
    }

    public async Task<bool> UploadFile(
        string fileName,
        IFormFile mediaFile,
        string storageIdentifier
    )
    {
        var containerClient = blobServiceClient.GetBlobContainerClient(storageIdentifier);

        var blobClient = containerClient.GetBlobClient(fileName);

        using (var stream = mediaFile.OpenReadStream())
        {
            await blobClient.UploadAsync(stream, overwrite: true);
        }

        return true;
    }

    public Uri GetFileUri(string fileName, string containerName)
    {
        var containerClient = blobServiceClient.GetBlobContainerClient(containerName);
        var blobClient = containerClient.GetBlobClient(fileName);

        return blobClient.Uri;
    }

    public async Task<bool> DeleteFile(string fileName, string containerName)
    {
        var containerClient = blobServiceClient.GetBlobContainerClient(containerName);
        var blobClient = containerClient.GetBlobClient(fileName);

        await blobClient.DeleteIfExistsAsync();

        return true;
    }

    public string GetBlobSasUri(string containerName, MediaType mediaType)
    {
        var containerClient = blobServiceClient.GetBlobContainerClient(containerName);

        var userId = currentUserContextService.GetUserId().ToString();

        string extension;
        string contentType;

        switch (mediaType)
        {
            case MediaType.Image:
                extension = ".jpeg";
                contentType = "image/jpeg";
                break;
            case MediaType.Video:
                extension = ".mp4";
                contentType = "video/mp4";
                break;
            default:
                throw new Exception("Invalid file type");
        }

        var fileName = Guid.NewGuid().ToString() + "_UserId_" + userId + extension;

        var blobClient = containerClient.GetBlobClient(fileName);

        BlobSasBuilder sasBuilder = new BlobSasBuilder()
        {
            BlobContainerName = containerClient.Name,
            BlobName = blobClient.Name,
            Resource = "b",
            ContentType = contentType,
            StartsOn = DateTimeOffset.UtcNow,
            ExpiresOn = DateTimeOffset.UtcNow.AddHours(1),
        };

        sasBuilder.SetPermissions(BlobSasPermissions.Write);

        string sasToken = sasBuilder
            .ToSasQueryParameters(
                new StorageSharedKeyCredential(blobServiceClient.AccountName, storageAccountKey)
            )
            .ToString();

        return blobClient.Uri + "?" + sasToken;
    }
}
