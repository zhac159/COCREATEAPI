using System.Text;
using Application.DTOs.AssetDTOs;
using Application.Extensions;
using Application.Interfaces;
using Azure.Storage.Blobs;
using Domain.Interfaces;

namespace Application.Services;

public class AssetService : IAssetService
{
    private readonly IAssetRepository assetRepository;
    private readonly BlobServiceClient blobStorageService;

    public AssetService(IAssetRepository assetRepository, BlobServiceClient blobStorageService)
    {
        this.assetRepository = assetRepository;
        this.blobStorageService = blobStorageService;
    }

    public async Task<AssetDTO> CreateAsync(AssetCreateWrapperDTO assetCreateWrapperDTO, int userId)
    {
        var fileSrc = new StringBuilder()
            .Append("asset_")
            .Append(userId)
            .Append("_")
            .Append(assetCreateWrapperDTO.Asset.Name)
            .Append("_")
            .Append(Guid.NewGuid().ToString())
            .Append(".jpeg") // Append the file extension
            .ToString();

        var mediaFile = assetCreateWrapperDTO.MediaFile;

        // Get a reference to the blob container
        var containerClient = blobStorageService.GetBlobContainerClient("assets");

        // Get a reference to a blob
        var blobClient = containerClient.GetBlobClient(fileSrc);

        // Open the file and upload its data
        using (var stream = mediaFile.OpenReadStream())
        {
            await blobClient.UploadAsync(stream, overwrite: true);
        }

        var asset = assetCreateWrapperDTO.Asset.ToEntity(fileSrc, userId);

        var createdAsset = await assetRepository.CreateAsync(asset);

        return createdAsset.ToDTO();
    }
}
