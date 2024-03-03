using Domain.Enums;

namespace Domain.Entities;

public class ProjectRoleMedia
{
    public int Id { get; set; }
    public required string Uri { get; set; }
    public required MediaType MediaType { get; set; }
    public required int Order { get; set; }
    public int ProjectRoleId { get; set; }
    public ProjectRole? ProjectRole { get; set; }
}