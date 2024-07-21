using System.ComponentModel.DataAnnotations;
using Application.DTOs.EnquiryDTOs;
using Application.DTOs.MediaDTOs;
using Application.DTOs.UserDtos;
using Domain.Enums;

namespace Application.DTOs.ProjectRoleDTOs;

public class ProjectRoleDTO
{
    [Required]
    public int Id { get; set; }

    [Required]
    public required string Name { get; set; }

    [Required]
    public required string Description { get; set; }

    [Required]
    public required int Cost { get; set; }

    [Required]
    public required int Effort { get; set; }

    [Required]
    public required DateTime StartDate { get; set; }

    [Required]
    public required DateTime EndDate { get; set; }

    [Required]
    public required SkillType SkillType { get; set; }

    [Required]
    public required double Longitude { get; set; }

    [Required]
    public required double Latitude { get; set; }

    [Required]
    public required string Address { get; set; }
    [Required]
    public int? ProjectId { get; set; }

    [Required]
    public List<string> Keywords { get; set; } = new List<string>();
    public required bool Remote { get; set; }
    public bool Completed { get; set; }
    public UserInformationDTO? Assignee { get; set; }
    public List<MediaDTO> Medias { get; set; } = new List<MediaDTO>();
    public List<EnquiryDTO> Enquiries { get; set; } = new List<EnquiryDTO>();
}
