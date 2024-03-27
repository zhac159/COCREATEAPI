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
    private readonly IStorageService storageService;

    public AssetService(
        IAssetRepository assetRepository,
        ICurrentUserContextService currentUserContextService,
        IStorageService storageService
    )
    {
        this.assetRepository = assetRepository;
        this.currentUserContextService = currentUserContextService;
        this.storageService = storageService;
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

        // Delete old files if the media has been removed
        if (assetUpdateDTO.Medias is not null)
        {
            var updateMediaIds = assetUpdateDTO.Medias.Select(m => m.Id).ToList();

            var mediasToDelete = asset.Medias.Where(m => !updateMediaIds.Contains(m.Id)).ToList();

            foreach (var media in mediasToDelete)
            {
                var fileName = Path.GetFileName(new Uri(media.Uri).LocalPath);

                await storageService.DeleteFile(fileName, "assets");
            }
        }

        // Delete old files if the uri has changed
        if (assetUpdateDTO.Medias is not null)
        {
            foreach (var updatedMedia in assetUpdateDTO.Medias)
            {
                var originalMedia = asset.Medias.FirstOrDefault(m => m.Id == updatedMedia.Id);

                if (originalMedia != null && originalMedia.Uri != updatedMedia.Uri)
                {
                    var fileName = Path.GetFileName(new Uri(originalMedia.Uri).LocalPath);
                    await storageService.DeleteFile(fileName, "assets");
                }
            }
        }

        asset.UpdateFromDTO(assetUpdateDTO);

        await assetRepository.UpdateAsync(asset);

        return asset.ToDTO();
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var asset = await assetRepository.GetByIdIncludeAllPropertiesAsync(id);

        if (asset is null)
        {
            throw new EntityNotFoundException();
        }

        if (asset.UserId != currentUserContextService.GetUserId())
        {
            throw new UnauthorizedAccessException();
        }

        foreach (var media in asset.Medias)
        {
            var fileName = Path.GetFileName(new Uri(media.Uri).LocalPath);

            await storageService.DeleteFile(fileName, "assets");
        }

        await assetRepository.DeleteAsync(asset);

        return true;
    }
    
}
