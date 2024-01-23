namespace Application.Interfaces;

    public interface IStorageService
    {
        Task<bool> UploadFile(string fileName, IFormFile mediaFile, string storageIdentifier);
        Uri GetFileUri(string fileName, string storageIdentifier);
        Task<bool> DeleteFile(string fileName, string storageIdentifier);
    }
