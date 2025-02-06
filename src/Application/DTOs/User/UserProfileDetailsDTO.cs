using System.ComponentModel.DataAnnotations;
using Application.DTOs.MediaDTOs;
using Application.DTOs.SkillDTOs;

namespace Application.DTOs.UserDtos;

public class UserProfileDetailsDTO
{
    [Required]
    public required string Username { get; set; }

    [Required]
    [EmailAddress]
    public required string Email { get; set; }

    [Required]
    public string? AboutYou { get; set; } = "";

    [Required]
    public required LocationDTO? Location { get; set; }

    [Required]
    public required MediaDTO ProfilePicture { get; set; }

    [Required]
    public List<SkillDTO> Skills { get; set; } = [];

    [Required]
    public List<MediaUpdateDTO> PortofolioMedias { get; set; } = [];
}
