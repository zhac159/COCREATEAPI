using NetTopologySuite.Geometries;

namespace Domain.Entities;

public class Project
{
    public int Id { get; set; }
    public DateTime? CompletedAt { get; set; }
    public required string Name { get; set; }
    public required string Description { get; set; }
    public required DateTime Date { get; set; }
    public required Point Location { get; set; }
    public required string Address { get; set; }
    public bool Completed { get; set; }
    public int ProjectManagerId { get; set; }
    public User? ProjectManager { get; set; }
    public List<ProjectRole> ProjectRoles { get; set; } = new List<ProjectRole>();
    public List<ProjectMedia> Medias { get; set; } = new List<ProjectMedia>();
    public List<Experience> Experiences { get; set; } = new List<Experience>();
    public List<AssetOffer> AssetOffers { get; set; } = new List<AssetOffer>();
}
