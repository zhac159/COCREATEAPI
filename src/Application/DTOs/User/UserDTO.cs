using Application.DTOs.AssetDTOs;
using Application.DTOs.PortofolioContentDTOs;
using Application.DTOs.ProjectDTOs;
using Application.DTOs.SkillDTOs;

namespace Application.DTOs.UserDtos;

public class UserDTO
{
    public int UserId { get; set; }
    public required string Username { get; set; }
    public required string Email { get; set; }
    public string? Address { get; set; }
    public double Latitude { get; set; }
    public double Longitude { get; set; }
    public string? AboutYou { get; set; }
    public int Coins { get; set; } = 0;
    public int Rating { get; set; } = 0;
    public int TotalReviews { get; set; } = 0;
    public string? ProfilePictureSrc { get; set; }
    public string? BannerPictureSrc { get; set; }
    public List<SkillDTO> Skills { get; set; } = new List<SkillDTO>();
    public List<PortofolioContentDTO>? PortofolioContents { get; set; }
    // public List<Review>? ReviewsGiven { get; set; }
    // public List<Review>? ReviewsReceived { get; set; }
    public List<AssetDTO>? Assets { get; set; }
    public List<ProjectDTO>? Projects { get; set; } = new List<ProjectDTO>();
}
