using Domain.Enums;

namespace Application.Interfaces;

    public interface IStorageService
    {
        Task<bool> UploadFile(string fileName, IFormFile mediaFile, string storageIdentifier);
        Uri GetFileUri(string fileName, string storageIdentifier);
        Task<bool> DeleteFile(string fileName, string storageIdentifier);
        string GetBlobSasUri(string containerName, MediaType mediaType);
    }
