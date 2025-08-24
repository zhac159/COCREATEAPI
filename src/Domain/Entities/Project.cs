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
    public List<ProjectRole> ProjectRoles { get; set; } = [];
    public List<ProjectMedia> Medias { get; set; } = [];
    public List<Experience> Experiences { get; set; } = [];
    public List<AssetOffer> AssetOffers { get; set; } = [];
}
