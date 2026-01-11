using System.ComponentModel.DataAnnotations;
using Application.DTOs.UserDtos;

namespace Application.DTOs.ReviewDTOs;

public class ReviewDTO
{
    [Required]
    public string? Description { get; set; }

    [Required]
    public double Rating { get; set; }

    [Required]
    public DateTime CreatedAt { get; set; }

    [Required]
    public UserInformationDTO? ReviewerUser { get; set; }
}
