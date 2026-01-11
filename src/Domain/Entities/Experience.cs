using Domain.Enums;

namespace Domain.Entities;

public class Experience
{
    public int Id { get; set; }
    public string? Description { get; set; }
    public int UserId { get; set; }
    public User? User { get; set; }
    public List<ExperienceMedia> Medias { get; set; } = [];
    public ExperienceType ExperienceType { get; set; }
    public int? ProjectRoleId { get; set; }
    public ProjectRole? ProjectRole { get; set; }
    public int? ProjectId { get; set; }
    public Project? Project { get; set; }
}