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
        var fileSrcs = new List<string>();

        for (int i = 0; i < assetCreateWrapperDTO.MediaFiles.Count; i++)
        {
            if(assetCreateWrapperDTO.Asset.Name == "null" || assetCreateWrapperDTO.Asset.Name == "undefined")
            {
                throw new InvalidModelException();
            }

            var fileSrc = new StringBuilder()
                .Append("asset_")
                .Append(userId)
                .Append("_")
                .Append(assetCreateWrapperDTO.Asset.Name)
                .Append("_")
                .Append(Guid.NewGuid().ToString())
                .Append(".jpeg")
                .ToString();

            await storageService.UploadFile(fileSrc, assetCreateWrapperDTO.MediaFiles[i], "assets");

            fileSrcs.Add(fileSrc);
        }

        var asset = assetCreateWrapperDTO.Asset.ToEntity(fileSrcs, userId);

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

        asset.FileSrcs.ForEach(async fileSrc =>
        {
            await storageService.DeleteFile(fileSrc, "assets");
        });

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
        

        if (assetUpdateWrapperDTO.AssetUpdateDTO.FileSrcs is not null)
        {
            var filesToDelete = asset.FileSrcs.Except(
                assetUpdateWrapperDTO.AssetUpdateDTO.FileSrcs
            );
            foreach (var fileSrc in filesToDelete)
            {
                await storageService.DeleteFile(fileSrc, "assets");
            }
        }

        if (
            assetUpdateWrapperDTO.MediaFiles is not null
            && assetUpdateWrapperDTO.AssetUpdateDTO.FileSrcs is not null
        )
        {
            for (int i = 0; i < assetUpdateWrapperDTO.MediaFiles.Count; i++)
            {
                var fileSrc = new StringBuilder()
                    .Append("asset_")
                    .Append(userId)
                    .Append("_")
                    .Append(asset.Name)
                    .Append("_")
                    .Append(Guid.NewGuid().ToString())
                    .Append(".jpeg")
                    .ToString();

                await storageService.UploadFile(
                    fileSrc,
                    assetUpdateWrapperDTO.MediaFiles[i],
                    "assets"
                );

                int index = assetUpdateWrapperDTO.AssetUpdateDTO.FileSrcs.IndexOf("placeholder");
                if (index != -1)
                {
                    assetUpdateWrapperDTO.AssetUpdateDTO.FileSrcs[index] = fileSrc;
                }
                else
                {
                    assetUpdateWrapperDTO.AssetUpdateDTO.FileSrcs.Add(fileSrc);
                }
            }
            assetUpdateWrapperDTO.AssetUpdateDTO.FileSrcs.RemoveAll(src => src == "placeholder");
        }

        asset.UpdateFromDTO(assetUpdateWrapperDTO.AssetUpdateDTO);

        await assetRepository.UpdateAsync(asset);

        return asset.ToDTO();
    }
}
