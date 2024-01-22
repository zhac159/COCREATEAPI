using Application.DTOs.AssetDTOs;
using Application.Interfaces;
using Domain.Entities;

namespace Application.Extensions;

public static class AssetExtensions
{
    public static AssetDTO ToDTO(this Asset asset, IStorageService storageService)
    {
        return new AssetDTO
        {
            Id = asset.Id,
            Name = asset.Name,
            Description = asset.Description,
            AssetType = asset.AssetType,
            Order = asset.Order,
            Cost = asset.Cost,
            DownloadUrl = storageService.GetFileUri(asset.FileSrc, "assets")
        };
    }
}