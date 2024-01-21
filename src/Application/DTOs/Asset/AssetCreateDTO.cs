using Domain.Entities;
using Domain.Enums;

namespace Application.DTOs.AssetDTOs;

public class AssetCreateDTO {
    public required string Name { get; set; }
    public required string Description { get; set; }
    public required AssetType AssetType { get; set; }
    public required int Order { get; set; }
    public required int Cost { get; set; }

    public Asset ToEntity(string fileSrc, int userId)
    {
        return new Asset
        {
            Name = Name,
            Description = Description,
            AssetType = AssetType,
            Order = Order,
            Cost = Cost,
            FileSrc = fileSrc,
            UserId = userId
        };
    }
}