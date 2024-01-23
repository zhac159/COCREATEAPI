using Application.DTOs.AssetDTOs;
using Application.Interfaces;
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
            Order = asset.Order,
            Cost = asset.Cost,
            Uri = asset.Uri ?? null
        };
    }

    public static void UpdateFromDTO(this Asset asset, AssetUpdateDTO assetUpdateDTO)
    {
        asset.Name = assetUpdateDTO.Name ?? asset.Name;
        asset.Description = assetUpdateDTO.Description ?? asset.Description;
        asset.AssetType = assetUpdateDTO.AssetType ?? asset.AssetType;
        asset.Order = assetUpdateDTO.Order ?? asset.Order;
        asset.Cost = assetUpdateDTO.Cost ?? asset.Cost;
    }
}