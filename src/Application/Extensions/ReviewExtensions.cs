using Application.DTOs.ReviewDTOs;
using Domain.Entities;

namespace Application.Extensions;

public static class ReviewExtensions
{
    public static ReviewDTO ToDTO(this Review review)
    {
        return new ReviewDTO
        {
            Description = review.Description,
            Rating = review.Rating,
            CreatedAt = review.CreatedAt,
            ReviewerUser = review.ReviewerUser?.ToInformationDTO()
        };
    }
}