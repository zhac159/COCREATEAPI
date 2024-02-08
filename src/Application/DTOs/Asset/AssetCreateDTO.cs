using Domain.Entities;
using Domain.Enums;

namespace Application.DTOs.AssetDTOs;

public class AssetCreateDTO {
    public required string Name { get; set; }
    public required string Description { get; set; }
    public required AssetType AssetType { get; set; }
    public required int Cost { get; set; }

    public Asset ToEntity(List<string> fileSrcs, int userId)
    {
        return new Asset
        {
            Name = Name,
            Description = Description,
            AssetType = AssetType,
            Cost = Cost,
            FileSrcs = fileSrcs,
            UserId = userId
        };
    }
}