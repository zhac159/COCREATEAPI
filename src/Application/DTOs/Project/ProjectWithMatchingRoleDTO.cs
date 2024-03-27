namespace Application.DTOs.ProjectDTOs;

public class ProjectWithMatchingRoleDTO
{
    public int ProjectRoleId { get; set; }
    public required ProjectDTO Project { get; set; }
}