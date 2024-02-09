using System.ComponentModel.DataAnnotations.Schema;
using Domain.Enums;

namespace Domain.Entities;

public class ProjectRole
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public required string Description { get; set; }
    public required List<string> FileSrcs { get; set; } = new List<string>();
    public required int Cost { get; set; }
    public required int Effort { get; set; }
    public required SkillType SkillType { get; set; }
    public double Longitude { get; set; }
    public double Latitude { get; set; }
    public required bool Remote { get; set; }
    public int? AssigneeId { get; set; }
    public User? Assignee { get; set; }
    public int ProjectId { get; set; }
    public Project? Project { get; set; }

    [NotMapped]
    public List<Uri> Uris { get; set; } = new List<Uri>();
}
