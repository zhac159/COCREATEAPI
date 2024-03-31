using Application.DTOs.PortofolioContentDTOs;
using Application.DTOs.SkillDTOs;

namespace Application.DTOs.UserDtos;

public class UserProfileDTO
{
    public int UserId { get; set; }
    public required string Username { get; set; }
    public string? AboutYou { get; set; }
    public int Rating { get; set; } = 0;
    public int TotalReviews { get; set; } = 0;
    public List<SkillDTO> Skills { get; set; } = new List<SkillDTO>();
    public List<PortofolioContentDTO>? PortofolioContents { get; set; } = new List<PortofolioContentDTO>();
}

public class UserProfilesDTO
{
    public required List<UserProfileDTO> UserProfiles { get; set; }
}
