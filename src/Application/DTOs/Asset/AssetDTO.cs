using Application.DTOs.MediaDTOs;
using Domain.Enums;

namespace Application.DTOs.AssetDTOs;

public class AssetDTO {
    public int Id { get; set; }
    public required string Name { get; set; }
    public required string Description { get; set; }
    public required AssetType AssetType { get; set; }
    public List<MediaDTO> Medias { get; set; } = new List<MediaDTO>();
}