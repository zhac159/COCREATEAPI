using Azure.Storage.Blobs;
using Azure.Storage.Sas;
using Infrastructure.Enums;
using Infrastructure.Interfaces;

namespace Infrastructure.Services;

public class AzureBlobStorageService(
    BlobServiceClient blobServiceClient,
    ICurrentUser currentUserContextService
) : IStorageService
{
    private const string ContainerName = "media";

    public async Task<bool> DeleteFileAsync(string blobPath)
    {
        var containerClient = blobServiceClient.GetBlobContainerClient(ContainerName);
        var blobClient = containerClient.GetBlobClient(blobPath);

        var result = await blobClient.DeleteIfExistsAsync();
        return result.Value;
    }

    public string GenerateUploadSasUri(string blobPrefix, MediaType mediaType)
    {
        var containerClient = blobServiceClient.GetBlobContainerClient(ContainerName);
        var userId = currentUserContextService.GetUserId();

        var (extension, contentType) = mediaType switch
        {
            MediaType.Image => (".jpeg", "image/jpeg"),
            MediaType.Video => (".mp4", "video/mp4"),
            MediaType.Audio => (".mp3", "audio/mpeg"),
            MediaType.Document => (".pdf", "application/pdf"),
            _ => throw new ArgumentException(
                $"Unsupported media type: {mediaType}",
                nameof(mediaType)
            ),
        };

        var blobPath = $"{blobPrefix}/{userId}/{Guid.NewGuid()}{extension}";
        var blobClient = containerClient.GetBlobClient(blobPath);

        var sasBuilder = new BlobSasBuilder
        {
            BlobContainerName = ContainerName,
            BlobName = blobPath,
            Resource = "b",
            ContentType = contentType,
            StartsOn = DateTimeOffset.UtcNow.AddMinutes(-5),
            ExpiresOn = DateTimeOffset.UtcNow.AddHours(1),
        };

        sasBuilder.SetPermissions(
            BlobSasPermissions.Create | BlobSasPermissions.Write | BlobSasPermissions.Read
        );

        if (!blobClient.CanGenerateSasUri)
        {
            throw new InvalidOperationException("BlobClient cannot generate SAS URI");
        }

        return blobClient.GenerateSasUri(sasBuilder).ToString();
    }
}
