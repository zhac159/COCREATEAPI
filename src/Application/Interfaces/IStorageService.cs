namespace Application.Interfaces;

    public interface IStorageService
    {
        Task<bool> UploadFile(string fileName, IFormFile mediaFile, string storageIdentifier);
    }
