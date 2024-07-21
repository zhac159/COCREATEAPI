using System.ComponentModel.DataAnnotations;

namespace Application.DTOs.ProjectDTOs;

public class ProjectWithMatchingRoleDTO
{
    [Required]
    public int ProjectRoleId { get; set; }

    [Required]
    public required ProjectDTO Project { get; set; }
}
