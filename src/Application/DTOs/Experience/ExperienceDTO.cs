using Application.DTOs.MediaDTOs;
using Application.DTOs.ProjectDTOs;
using Application.DTOs.ProjectRoleDTOs;
using Application.DTOs.UserDtos;
using Domain.Enums;

namespace Application.DTOs.ExperienceDTOs;

public class ExperienceDTO
{
    public int Id { get; set; }
    public string? Description { get; set; }
    public int UserId { get; set; }
    public UserDTO? User { get; set; }
    public List<MediaDTO> Medias { get; set; } = new();
    public ExperienceType ExperienceType { get; set; }
    public int ProjectRoleId { get; set; }
    public ProjectRoleDTO? ProjectRole { get; set; }
    public int ProjectId { get; set; }
    public ProjectDTO? Project { get; set; }
}