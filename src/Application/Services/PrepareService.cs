using Application.DTOs.PrepareDTOs;
using Application.Interfaces;

namespace Application.Services;

public class PrepareService : IPrepareService
{
    private readonly IStorageService storageService;

    public PrepareService(IStorageService storageService)
    {
        this.storageService = storageService;
    }

    public PrepareUploadResponseDTO Upload(List<PrepareUploadDTO> prepareUploadDTO)
    {
        var sasURIs = new List<string>();

        foreach (var prepareUpload in prepareUploadDTO)
        {
            var containerName = prepareUpload.Entity.ToString().ToLower() + "s";

            var sasURI = storageService.GetBlobSasUri(containerName, prepareUpload.MediaType);

            sasURIs.Add(sasURI);
        }

        return new PrepareUploadResponseDTO { SasURIs = sasURIs };
    }
}
