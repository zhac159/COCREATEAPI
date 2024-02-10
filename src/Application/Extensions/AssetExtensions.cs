using Application.DTOs.AssetDTOs;
using Domain.Entities;

namespace Application.Extensions;

public static class AssetExtensions
{
    public static AssetDTO ToDTO(this Asset asset)
    {
        return new AssetDTO
        {
            Id = asset.Id,
            Name = asset.Name,
            Description = asset.Description,
            AssetType = asset.AssetType,
            Cost = asset.Cost,
            Uris = asset.Uris
        };
    }

    public static void UpdateFromDTO(this Asset asset, AssetUpdateDTO assetUpdateDTO)
    {
        asset.Name = assetUpdateDTO.Name ?? asset.Name;
        asset.Description = assetUpdateDTO.Description ?? asset.Description;
        asset.AssetType = assetUpdateDTO.AssetType ?? asset.AssetType;
        asset.Cost = assetUpdateDTO.Cost ?? asset.Cost;
        asset.FileSrcs = assetUpdateDTO.FileSrcs ?? asset.FileSrcs;
    }
}