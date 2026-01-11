using System.ComponentModel.DataAnnotations;
using Application.DTOs.AssetDTOs;
using Application.DTOs.ProjectDTOs;

namespace Application.DTOs.AssetOfferDTOs;

public class AssetOfferDTO
{
    [Required]
    public int Id { get; set; }

    [Required]
    public required int OfferValue { get; set; }

    [Required]
    public required DateTime AssetUsageStartTime { get; set; }

    [Required]
    public required DateTime AssetUsageEndTime { get; set; }

    [Required]
    public required int Duration { get; set; }

    [Required]
    public string? Description { get; set; }
    public AssetInformationDTO? Asset { get; set; }
    public ProjectInformationDTO? Project { get; set; }
}
