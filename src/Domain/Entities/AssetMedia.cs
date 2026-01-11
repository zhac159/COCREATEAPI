using Domain.Enums;

namespace Domain.Entities;

public class AssetMedia
{
    public int Id { get; set; }
    public required string Uri { get; set; }
    public required MediaType MediaType { get; set; }
    public required int Order { get; set; }
    public int AssetId { get; set; }
    public Asset? Asset { get; set; }
}