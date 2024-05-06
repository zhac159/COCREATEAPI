using System.ComponentModel.DataAnnotations;
using Application.DTOs.MediaDTOs;
using Domain.Enums;

namespace Application.DTOs.AssetDTOs;

public class AssetDTO
{
    [Required]
    public int Id { get; set; }

    [Required]
    public required string Name { get; set; }

    [Required]
    public required string Description { get; set; }

    [Required]
    public required AssetType AssetType { get; set; }

    [Required]
    public List<MediaDTO> Medias { get; set; } = new List<MediaDTO>();
}
