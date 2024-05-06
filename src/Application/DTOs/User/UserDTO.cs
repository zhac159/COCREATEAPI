using System.ComponentModel.DataAnnotations;
using Application.DTOs.AssetDTOs;
using Application.DTOs.AssetOfferDTOs;
using Application.DTOs.EnquiryDTOs;
using Application.DTOs.ExperienceDTOs;
using Application.DTOs.PortofolioContentDTOs;
using Application.DTOs.ProjectDTOs;
using Application.DTOs.ReviewDTOs;
using Application.DTOs.SkillDTOs;

namespace Application.DTOs.UserDtos;

public class UserDTO
{
    [Required]
    public int UserId { get; set; }

    [Required]
    public required string Username { get; set; }

    [Required]
    public required string Email { get; set; }
    public string? Address { get; set; }
    public double Latitude { get; set; }
    public double Longitude { get; set; }
    public string? AboutYou { get; set; }
    public int Coins { get; set; } = 0;
    public string? ProfilePictureSrc { get; set; }
    public string? BannerPictureSrc { get; set; }
    public string? PublicKey { get; set; }

    [Required]
    public double Rating { get; set; } = 0;

    [Required]
    public int TotalReviews { get; set; } = 0;

    [Required]
    public List<SkillDTO> Skills { get; set; } = new List<SkillDTO>();

    [Required]
    public List<PortofolioContentDTO>? PortofolioContents { get; set; }

    [Required]
    public List<ProjectDTO> AssignedProjects { get; set; } = new List<ProjectDTO>();

    [Required]
    public List<ReviewDTO>? ReviewsReceived { get; set; } = new List<ReviewDTO>();

    [Required]
    public List<AssetDTO>? Assets { get; set; }

    [Required]
    public List<ProjectDTO>? Projects { get; set; } = new List<ProjectDTO>();

    [Required]
    public List<EnquiryDTO>? Enquiries { get; set; } = new List<EnquiryDTO>();

    [Required]
    public List<ExperienceDTO>? Experiences { get; set; } = new List<ExperienceDTO>();

    [Required]
    public List<AssetOfferDTO>? AssetOffers { get; set; } = new List<AssetOfferDTO>();
}
