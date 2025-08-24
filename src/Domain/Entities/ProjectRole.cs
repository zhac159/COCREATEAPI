using Domain.Enums;
using NetTopologySuite.Geometries;

namespace Domain.Entities;

public class ProjectRole
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public required string Description { get; set; }
    public required int Cost { get; set; }
    public required SkillType SkillType { get; set; }
    public required int Effort { get; set; }
    public required Point Location { get; set; }
    public required string Address { get; set; }
    public required List<string> Keywords { get; set; } = [];
    public required bool Remote { get; set; }
    public int? AssigneeId { get; set; }
    public User? Assignee { get; set; }
    public int ProjectId { get; set; }
    public bool Completed { get; set; }
    public Project? Project { get; set; }
    public List<SeenMatches> SeenMatches { get; set; } = [];
    public List<Enquiry> Enquiries { get; set; } = [];
    public List<ProjectRoleMedia> Medias { get; set; } = [];
    public List<Experience> Experiences { get; set; } = [];
}
