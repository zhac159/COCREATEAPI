using Infrastructure.Enums;

namespace Infrastructure.Interfaces;

public interface IStorageService
{
    Task<bool> DeleteFileAsync(string blobPath);
    string GenerateUploadSasUri(string blobPrefix, MediaType mediaType);
}
