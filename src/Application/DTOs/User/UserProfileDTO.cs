using System.ComponentModel.DataAnnotations;
using Application.DTOs.ExperienceDTOs;
using Application.DTOs.PortofolioContentDTOs;
using Application.DTOs.ReviewDTOs;
using Application.DTOs.SkillDTOs;

namespace Application.DTOs.UserDtos;

public class UserProfileDTO
{
    [Required]
    public int UserId { get; set; }

    [Required]
    public required string Username { get; set; }

    [Required]
    public string? AboutYou { get; set; }

    [Required]
    public double Rating { get; set; } = 0;

    [Required]
    public int TotalReviews { get; set; } = 0;

    [Required]
    public List<ReviewDTO> ReviewsReceived { get; set; } = new List<ReviewDTO>();

    [Required]
    public List<SkillDTO> Skills { get; set; } = new List<SkillDTO>();

    [Required]
    public List<PortofolioContentDTO>? PortofolioContents { get; set; } =
        new List<PortofolioContentDTO>();

    [Required]
    public List<ExperienceDTO>? Experiences { get; set; } = new List<ExperienceDTO>();
}

public class UserProfilesDTO
{
    [Required]
    public required List<UserProfileDTO> UserProfiles { get; set; } = new List<UserProfileDTO>();
}
