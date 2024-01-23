using Domain.Enums;

namespace Application.DTOs.AssetDTOs;

public class AssetDTO {
    public int Id { get; set; }
    public required string Name { get; set; }
    public required string Description { get; set; }
    public required AssetType AssetType { get; set; }
    public required int Order { get; set; }
    public required int Cost { get; set; }
    public required Uri? Uri { get; set; }
}