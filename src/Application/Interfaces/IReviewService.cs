using Domain.Entities;

namespace Application.Interfaces;

public interface IReviewService
{
    // Task<ReviewDTO> CreateAsync(ReviewCreateDTO review);
    Task<bool> CreateRangeFromEntitiesAsync(List<Review> reviews);
}