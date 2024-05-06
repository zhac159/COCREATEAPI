using System.ComponentModel.DataAnnotations;
using Application.DTOs.AssetOfferDTOs;
using Application.DTOs.MediaDTOs;
using Application.DTOs.ProjectRoleDTOs;
using Application.DTOs.UserDtos;

namespace Application.DTOs.ProjectDTOs;

public class ProjectDTO
{
    [Required]
    public int Id { get; set; }

    [Required]
    public required string Name { get; set; }

    [Required]
    public required string Description { get; set; }

    [Required]
    public bool Completed { get; set; }

    [Required]
    public List<MediaDTO> Medias { get; set; } = new List<MediaDTO>();

    [Required]
    public UserInformationDTO? ProjectManager { get; set; }

    [Required]
    public List<ProjectRoleDTO> ProjectRoles { get; set; } = new List<ProjectRoleDTO>();

    [Required]
    public List<AssetOfferDTO>? AssetOffers { get; set; } = new List<AssetOfferDTO>();
}
