using System.ComponentModel.DataAnnotations;
using Domain.Entities;

namespace Application.DTOs.AssetOfferDTOs;

public class AssetOfferCreateDTO
{
    [Required]
    public required int OfferValue { get; set; }

    [Required]
    public required DateTime AssetUsageStartTime { get; set; }

    [Required]
    public required DateTime AssetUsageEndTime { get; set; }

    [Required]
    public required int Duration { get; set; }

    public string? Description { get; set; }

    [Required]
    public required int AssetId { get; set; }

    [Required]
    public required int ProjectId { get; set; }

    public AssetOffer ToEntity()
    {
        return new AssetOffer
        {
            OfferValue = OfferValue,
            CreatedAt = DateTime.UtcNow,
            AssetUsageStartTime = AssetUsageStartTime,
            AssetUsageEndTime = AssetUsageEndTime,
            Duration = Duration,
            Description = Description,
            AssetId = AssetId,
            ProjectId = ProjectId
        };
    }
}
