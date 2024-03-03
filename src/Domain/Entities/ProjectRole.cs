using Domain.Enums;
using NetTopologySuite.Geometries;

namespace Domain.Entities;

public class ProjectRole
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public required string Description { get; set; }
    public required int Cost { get; set; }
    public required int Effort { get; set; }
    public required DateTime StartDate { get; set; }
    public required DateTime EndDate { get; set; }
    public required SkillType SkillType { get; set; }
    public required Point Location { get; set; }
    public required string Address { get; set; }
    public required List<string> Keywords { get; set; } = new List<string>();
    public required bool Remote { get; set; }
    public int? AssigneeId { get; set; }
    public User? Assignee { get; set; }
    public int ProjectId { get; set; }
    public Project? Project { get; set; }
    public List<SeenMatches> SeenMatches { get; set; } = new List<SeenMatches>();
    public List<Enquiry> Enquiries { get; set; } = new List<Enquiry>();
    public List<ProjectRoleMedia> Medias { get; set; } = new List<ProjectRoleMedia>();
}
