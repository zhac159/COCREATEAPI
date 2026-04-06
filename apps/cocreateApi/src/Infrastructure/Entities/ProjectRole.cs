using Infrastructure.Enums;

namespace Infrastructure.Entities;

public class ProjectRole
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public required string Description { get; set; }
    public required int Cost { get; set; }
    public required SkillType SkillType { get; set; }
    public required bool Remote { get; set; }
    public bool Completed { get; set; }
    public int ProjectId { get; set; }
    public Project? Project { get; set; }
    public int? AssigneeId { get; set; }
    public User? Assignee { get; set; }
}
