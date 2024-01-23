using Domain.Enums;

namespace Application.DTOs.AssetDTOs;

public class AssetUpdateDTO {
    public int Id { get; set; }
    public string? Name { get; set; }
    public string? Description { get; set; }
    public AssetType? AssetType { get; set; }
    public int? Order { get; set; }
    public int? Cost { get; set; }
}