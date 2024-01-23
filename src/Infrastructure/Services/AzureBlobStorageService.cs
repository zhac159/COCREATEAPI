using Application.Interfaces;
using Azure.Storage.Blobs;

namespace Infrastructure.Services;

public class AzureBlobStorageService : IStorageService
{
    private readonly BlobServiceClient blobServiceClient;

    public AzureBlobStorageService(BlobServiceClient blobServiceClient)
    {
        this.blobServiceClient = blobServiceClient;
    }

    public async Task<bool> UploadFile(
        string fileName,
        IFormFile mediaFile,
        string storageIdentifier
    )
    {
        var containerClient = blobServiceClient.GetBlobContainerClient(storageIdentifier);

        // Get a reference to a blob
        var blobClient = containerClient.GetBlobClient(fileName);

        // Open the file and upload its data
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
}