using System.ComponentModel.DataAnnotations.Schema;
using Domain.Enums;

namespace Domain.Entities;

public class Asset
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public required string Description { get; set; }
    public required AssetType AssetType { get; set; }
    public required List<string> FileSrcs { get; set; } = new List<string>();
    public int Cost { get; set; }
    public int UserId { get; set; }
    public User? User { get; set; }

    [NotMapped]
    public List<Uri> Uris { get; set; } = new List<Uri>();
}
