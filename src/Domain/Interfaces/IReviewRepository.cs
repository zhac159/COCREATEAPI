using Domain.Entities;

namespace Domain.Interfaces;

public interface IReviewRepository
{
    Task<Review> CreateAsync(Review review);
    Task<List<Review>> CreateRangeAsync(List<Review> reviews);
}