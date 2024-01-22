using System.Text;
using Application.DTOs.AssetDTOs;
using Application.Extensions;
using Application.Interfaces;
using Domain.Interfaces;

namespace Application.Services;

public class AssetService : IAssetService
{
    private readonly IAssetRepository assetRepository;
    private readonly IStorageService storageService;

    public AssetService(IAssetRepository assetRepository, IStorageService storageService)
    {
        this.assetRepository = assetRepository;
        this.storageService = storageService;
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

        var uploaded = await storageService.UploadFile(fileSrc, mediaFile, "assets");

        var asset = assetCreateWrapperDTO.Asset.ToEntity(fileSrc, userId);

        var createdAsset = await assetRepository.CreateAsync(asset);

        return createdAsset.ToDTO(storageService);
    }
}
