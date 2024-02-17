using System.Text.Json;
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
            Medias = asset
                .Medias.OrderBy(media => media.Order)
                .Select(media => media.ToDTO())
                .ToList()
        };
    }

    public static void UpdateFromDTO(this Asset asset, AssetUpdateDTO assetUpdateDTO)
    {
        asset.Name = assetUpdateDTO.Name ?? asset.Name;
        asset.Description = assetUpdateDTO.Description ?? asset.Description;
        asset.AssetType = assetUpdateDTO.AssetType ?? asset.AssetType;

        if (assetUpdateDTO.Medias is not null)
        {
            asset.Medias?.RemoveAll(m => !assetUpdateDTO.Medias.Any(mu => mu.Id == m.Id));

            asset.Medias = assetUpdateDTO
                .Medias.Select(
                    (mediaUpdateDTO, order) =>
                    {
                        var media = asset.Medias?.FirstOrDefault(m => m.Id == mediaUpdateDTO.Id);
                        if (media is not null)
                        {
                            media.UpdateFromDTO(mediaUpdateDTO, order);
                            return media;
                        }
                        return mediaUpdateDTO.ToAssetMediaEntity(order);
                    }
                )
                .ToList();
        }
    }
}
