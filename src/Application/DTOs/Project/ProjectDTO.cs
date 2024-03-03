using Application.DTOs.MediaDTOs;
using Application.DTOs.ProjectRoleDTOs;
using Application.DTOs.UserDtos;

namespace Application.DTOs.ProjectDTOs;

public class ProjectDTO
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public required string Description { get; set; }
    public List<MediaDTO> Medias { get; set; } = new List<MediaDTO>();
    public UserInformationDTO? ProjectManager { get; set; }
    public List<ProjectRoleDTO> ProjectRoles { get; set; } = new List<ProjectRoleDTO>();
}
