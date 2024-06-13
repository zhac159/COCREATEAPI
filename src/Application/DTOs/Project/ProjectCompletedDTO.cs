using System.ComponentModel.DataAnnotations;
using Application.DTOs.MediaDTOs;
using Application.DTOs.ProjectRoleDTOs;
using Application.DTOs.UserDtos;

namespace Application.DTOs.ProjectDTOs;

public class ProjectCompletedDTO
{
    [Required]
    public int Id { get; set; }

    [Required]
    public required string Name { get; set; }

    [Required]
    public required string Description { get; set; }

    [Required]
    public required List<MediaDTO> Medias { get; set; } = new List<MediaDTO>();

    [Required]
    public required List<MediaDTO> ExperiencesMedias { get; set; } = new List<MediaDTO>();

    [Required]
    public required UserInformationDTO ProjectManager { get; set; }

    [Required]
    public required List<ProjectRoleDTO> ProjectRoles { get; set; } = new List<ProjectRoleDTO>();
}
