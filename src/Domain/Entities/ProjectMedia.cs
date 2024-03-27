using Domain.Enums;

namespace Domain.Entities;

public class ProjectMedia
{
    public int Id { get; set; }
    public required string Uri { get; set; }
    public required MediaType MediaType { get; set; }
    public required int Order { get; set; }
    public int ProjectId { get; set; }
    public Project? Project { get; set; }
}