using System.ComponentModel.DataAnnotations;
using Application.DTOs.MediaDTOs;
using Application.DTOs.ProjectDTOs;
using Application.DTOs.ProjectRoleDTOs;
using Application.DTOs.UserDtos;
using Domain.Enums;

namespace Application.DTOs.ExperienceDTOs;

public class ExperienceDTO
{
    [Required]
    public int Id { get; set; }
    [Required]
    public string? Description { get; set; }
    [Required]
    public int UserId { get; set; }
    [Required]
    public UserDTO? User { get; set; }
    [Required]
    public List<MediaDTO> Medias { get; set; } = new();
    [Required]
    public ExperienceType ExperienceType { get; set; }
    [Required]
    public int? ProjectRoleId { get; set; }
    [Required]
    public ProjectRoleDTO? ProjectRole { get; set; }
    [Required]
    public int? ProjectId { get; set; }
    [Required]
    public ProjectDTO? Project { get; set; }
}