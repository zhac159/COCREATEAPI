namespace Domain.Entities;

public class AssetOffer
{
    public int Id { get; set; }
    public required int OfferValue { get; set; }
    public required DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public required DateTime AssetUsageStartTime { get; set; }
    public required DateTime AssetUsageEndTime { get; set; }
    public required int Duration { get; set; }
    public string? Description { get; set; }
    public required int AssetId { get; set; }
    public Asset? Asset { get; set; }
    public required int ProjectId { get; set; }
    public Project? Project { get; set; }
}
