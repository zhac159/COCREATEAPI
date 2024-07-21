using System.ComponentModel.DataAnnotations;

namespace Application.DTOs.ProjectDTOs;

public class ProjectWithMatchingRolesListDTO
{
    [Required]
    public List<ProjectWithMatchingRoleDTO> ProjectWithMatchingRoles { get; set; } = new List<ProjectWithMatchingRoleDTO>();
}