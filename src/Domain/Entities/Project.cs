namespace Domain.Entities;

public class Project
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public required string Description { get; set; }
    public required List<string> FileSrcs { get; set; } = new List<string>();
    public int ProjectManagerId { get; set; }
    public User? ProjectManager { get; set; }
    public List<SubProject> SubProjects { get; set; } = new List<SubProject>();
}
