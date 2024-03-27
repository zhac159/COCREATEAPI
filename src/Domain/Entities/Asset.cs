using Domain.Enums;

namespace Domain.Entities;

public class Asset
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public required string Description { get; set; }
    public required AssetType AssetType { get; set; }
    public int UserId { get; set; }
    public User? User { get; set; }
    public List<AssetMedia> Medias { get; set; } = new List<AssetMedia>();
}
