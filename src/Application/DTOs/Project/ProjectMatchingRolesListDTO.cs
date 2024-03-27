namespace Application.DTOs.ProjectDTOs;

public class ProjectWithMatchingRolesListDTO
{
    public List<ProjectWithMatchingRoleDTO> ProjectWithMatchingRoles { get; set; } = new List<ProjectWithMatchingRoleDTO>();
}