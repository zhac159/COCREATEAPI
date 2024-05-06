using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Persistence;

namespace Infrastructure.Repositories;

public class ReviewRepository : IReviewRepository
{
    private readonly CoCreateDbContext context;

    public ReviewRepository(CoCreateDbContext context)
    {
        this.context = context;
    }

    public async Task<Review> CreateAsync(Review review)
    {
        await context.Reviews.AddAsync(review);
        await context.SaveChangesAsync();

        return review;
    }

    public async Task<List<Review>> CreateRangeAsync(List<Review> reviews)
    {
        await context.Reviews.AddRangeAsync(reviews);
        await context.SaveChangesAsync();

        return reviews;
    }
}
