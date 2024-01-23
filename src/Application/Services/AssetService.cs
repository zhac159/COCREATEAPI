using System.Text;
using Application.DTOs.AssetDTOs;
using Application.Extensions;
using Application.Interfaces;
using Domain.Exceptions;
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
            .Append(".jpeg")
            .ToString();

        var mediaFile = assetCreateWrapperDTO.MediaFile;

        var uploaded = await storageService.UploadFile(fileSrc, mediaFile, "assets");

        var asset = assetCreateWrapperDTO.Asset.ToEntity(fileSrc, userId);

        var createdAsset = await assetRepository.CreateAsync(asset);

        return createdAsset.ToDTO();
    }

    public async Task<bool> DeleteAsync(int id, int userId)
    {
        var asset = await assetRepository.GetByIdAsync(id);

        if (asset is null)
        {
            throw new EntityNotFoundException();
        }

        await storageService.DeleteFile(asset.FileSrc, "assets");

        await assetRepository.DeleteAsync(asset);

        return true;
    }

    public async Task<AssetDTO> UpdateAsync(AssetUpdateWrapperDTO assetUpdateWrapperDTO, int userId)
    {
        var asset = await assetRepository.GetByIdAsync(assetUpdateWrapperDTO.AssetUpdateDTO.Id);

        if (asset is null)
        {
            throw new EntityNotFoundException();
        }

        if (assetUpdateWrapperDTO.MediaFile is not null)
        {

            await storageService.UploadFile(asset.FileSrc, assetUpdateWrapperDTO.MediaFile, "assets");
        }

        asset.UpdateFromDTO(assetUpdateWrapperDTO.AssetUpdateDTO);

        await assetRepository.UpdateAsync(asset);

        return asset.ToDTO();
    }
}
