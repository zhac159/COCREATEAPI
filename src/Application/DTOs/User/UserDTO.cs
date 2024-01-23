using Application.DTOs.AssetDTOs;
using Application.DTOs.PortofolioContentDTOs;
using Application.DTOs.SkillDTOs;
using Domain.Entities;

namespace Application.DTOs.UserDtos;

public class UserDTO
{
    public int UserId { get; set; }
    public required string Username { get; set; }
    public required string Email { get; set; }
    public required string Location { get; set; }
    public string? AboutYou { get; set; }
    public int Coins { get; set; } = 0;
    public int Rating { get; set; } = 0;
    public int TotalReviews { get; set; } = 0;
    public string? ProfilePictureSrc { get; set; }
    public string? BannerPictureSrc { get; set; }
    public List<SkillDTO>? Skills { get; set; }
    public List<PortofolioContentDTO>? PortofolioContents { get; set; }
    public List<Review>? ReviewsGiven { get; set; }
    public List<Review>? ReviewsReceived { get; set; }
    public List<AssetDTO>? Assets { get; set; }
}
