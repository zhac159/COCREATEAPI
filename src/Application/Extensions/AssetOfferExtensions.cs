using Application.DTOs.AssetOfferDTOs;
using Domain.Entities;

namespace Application.Extensions;

public static class AssetOfferExtensions
{
    public static AssetOfferDTO ToDTO(this AssetOffer assetOffer)
    {
        return new AssetOfferDTO
        {
            Id = assetOffer.Id,
            OfferValue = assetOffer.OfferValue,
            AssetUsageStartTime = assetOffer.AssetUsageStartTime,
            AssetUsageEndTime = assetOffer.AssetUsageEndTime,
            Duration = assetOffer.Duration,
            Description = assetOffer.Description,
            Asset = assetOffer.Asset?.ToInformationDTO(),
            Project = assetOffer.Project?.ToInformationDTO()
        };
    }
}