using System.Reflection.Metadata;
using Application.DTOs.AssetDTOs;
using Azure.Storage.Blobs;
using Domain.Entities;

namespace Application.Extensions;

public static class AssetExtensions
{
    // public static void UpdateFromDTO(this Asset asset, AssetUpdateDTO assetUpdateDTO)
    // {
    //     asset.Name = assetUpdateDTO.Name;
    //     asset.Description = assetUpdateDTO.Description;
    //     asset.AssetType = assetUpdateDTO.AssetType;
    //     asset.Order = assetUpdateDTO.Order;
    //     asset.Cost = assetUpdateDTO.Cost;
    // }

    private readonly BlobServiceClient blobStorageService;

    public static AssetDTO ToDTO(this Asset asset)
    {
        return new AssetDTO
        {
            Id = asset.Id,
            Name = asset.Name,
            Description = asset.Description,
            AssetType = asset.AssetType,
            Order = asset.Order,
            Cost = asset.Cost
        };
    }
}