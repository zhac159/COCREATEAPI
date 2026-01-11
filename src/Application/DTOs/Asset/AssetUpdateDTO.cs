using Application.DTOs.MediaDTOs;
using Domain.Enums;

namespace Application.DTOs.AssetDTOs;

public class AssetUpdateDTO
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public string? Description { get; set; }
    public AssetType? AssetType { get; set; }
    public List<MediaUpdateDTO>? Medias { get; set; }
}
