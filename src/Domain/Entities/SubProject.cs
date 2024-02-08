namespace Domain.Entities;

public class SubProject
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public required string Description { get; set; }
    public required List<string> FileSrcs { get; set; } = new List<string>();
    public required int Cost { get; set; }
    public required int Effort { get; set; }
    public required Skill Skill { get; set; }
    public double Longitude { get; set; }
    public double Latitude { get; set; }
    public required bool Remote { get; set; }
    public int? AssigneeId { get; set; }
    public User? Assignee { get; set; }
    public int ProjectId { get; set; }
    public Project? Project { get; set; }
}
