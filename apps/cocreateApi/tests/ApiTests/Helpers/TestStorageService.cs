using Azure.Storage.Blobs;
using Infrastructure.Enums;
using Infrastructure.Interfaces;
using Infrastructure.Services;

namespace ApiTests.Helpers;

/// <summary>
/// Replaces AzureBlobStorageService in integration tests.
/// SAS URI generation is kept real (purely client-side crypto, no network call).
/// DeleteFileAsync is stubbed out so tests don't need Azurite running.
/// </summary>
public class TestStorageService(BlobServiceClient blobServiceClient, ICurrentUser currentUser)
    : IStorageService
{
    private readonly AzureBlobStorageService _inner = new(blobServiceClient, currentUser);

    public Task<bool> DeleteFileAsync(string blobPath) => Task.FromResult(true);

    public string GenerateUploadSasUri(string blobPrefix, MediaType mediaType) =>
        _inner.GenerateUploadSasUri(blobPrefix, mediaType);
}
