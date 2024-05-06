using Domain.Entities;

namespace Application.DTOs.ReviewDTOs;

public class ReviewCreateDTO
{
    public string? Description { get; set; }
    public double Rating { get; set; }
    public int ReviewedUserId { get; set; }

    public Review ToEntity(int reviewerUserId)
    {
        return new Review
        {
            Description = Description,
            Rating = Rating,
            ReviewedUserId = ReviewedUserId,
            ReviewerUserId = reviewerUserId
        };
    }
}
