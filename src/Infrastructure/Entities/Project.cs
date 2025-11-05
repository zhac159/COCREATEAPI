using NetTopologySuite.Geometries;

namespace Infrastructure.Entities;

public class Project
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public required string Description { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public required DateTime Date { get; set; }
    public required Point Location { get; set; }
    public bool Completed { get; set; }
    public DateTime? CompletedAt { get; set; }
    public int ProjectManagerId { get; set; }
    public User? ProjectManager { get; set; }
    public List<ProjectRole> ProjectRoles { get; set; } = [];
    public List<ProjectMedia> ProjectMedias { get; set; } = [];
}
