using Application.DTOs.MediaDTOs;
using Domain.Entities;
using Domain.Enums;

namespace Application.DTOs.AssetDTOs;

public class AssetCreateDTO {
    public required string Name { get; set; }
    public required string Description { get; set; }
    public required AssetType AssetType { get; set; }
    public required List<MediaCreateDTO> Medias { get; set; }

    public Asset ToEntity(int userId)
    {
        return new Asset
        {
            Name = Name,
            Description = Description,
            AssetType = AssetType,
            UserId = userId,
            Medias = Medias.Select((m, order) => m.ToEntity(order)).ToList()
        };
    }
}