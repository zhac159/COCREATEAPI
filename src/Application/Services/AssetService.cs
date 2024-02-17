using System.Runtime.Serialization;
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
    private readonly ICurrentUserContextService currentUserContextService;

    public AssetService(
        IAssetRepository assetRepository,
        ICurrentUserContextService currentUserContextService
    )
    {
        this.assetRepository = assetRepository;
        this.currentUserContextService = currentUserContextService;
    }

    public async Task<AssetDTO> CreateAsync(AssetCreateDTO assetCreateDTO)
    {
        var asset = assetCreateDTO.ToEntity(currentUserContextService.GetUserId());

        var createdAsset = await assetRepository.CreateAsync(asset);

        return createdAsset.ToDTO();
    }

    public async Task<AssetDTO> UpdateAsync(AssetUpdateDTO assetUpdateDTO)
    {   
        var asset = await assetRepository.GetByIdIncludeAllPropertiesAsync(assetUpdateDTO.Id);

        if (asset is null)
        {
            throw new EntityNotFoundException();
        }

        if (asset.UserId != currentUserContextService.GetUserId())
        {
            throw new UnauthorizedAccessException();
        }

        asset.UpdateFromDTO(assetUpdateDTO);

        await assetRepository.UpdateAsync(asset);

        return asset.ToDTO();
    }

    // public async Task<bool> DeleteAsync(int id, int userId)
    // {
    //     var asset = await assetRepository.GetByIdAsync(id);

    //     if (asset is null)
    //     {
    //         throw new EntityNotFoundException();
    //     }

    //     asset.FileSrcs.ForEach(async fileSrc =>
    //     {
    //         await storageService.DeleteFile(fileSrc, "assets");
    //     });

    //     await assetRepository.DeleteAsync(asset);

    //     return true;
    // }

}
