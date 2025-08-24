using System.ComponentModel.DataAnnotations;
using Application.DTOs.MediaDTOs;
using Application.DTOs.SkillDTOs;

namespace Application.DTOs.UserDtos;

public class UserUpdateDTO
{
    [Required]
    public required string Username { get; set; }

    [Required]
    [EmailAddress]
    public required string Email { get; set; }

    [Required]
    public required string AboutYou { get; set; }

    [Required]
    public required LocationDTO Location { get; set; }

    [Required]
    public required MediaUpdateDTO ProfilePicture { get; set; }

    [Required]
    public List<SkillUpdateDTO> Skills { get; set; } = [];

    [Required]
    public List<MediaUpdateDTO> PortfolioMedias { get; set; } = [];
}
