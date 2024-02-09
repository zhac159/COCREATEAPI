using Application.DTOs.ProjectRoleDTOs;
using Application.DTOs.UserDtos;

namespace Application.DTOs.ProjectDTO;

public class ProjectDTO
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public required string Description { get; set; }
    public required List<Uri> Uris { get; set; }
    public UserInformationDTO? ProjectManager { get; set; }
    public List<ProjectRoleDTO> ProjectRoles { get; set; } = new List<ProjectRoleDTO>();
}