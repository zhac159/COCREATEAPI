using Application.Interfaces;
using Domain.Entities;
using Domain.Exceptions;
using Domain.Interfaces;

namespace Application.Services;

public class ReviewService : IReviewService
{
    private readonly IReviewRepository reviewRepository;
    private readonly IUserRepository userRepository;
    private readonly ICurrentUserContextService currentUserContextService;

    public ReviewService(
        IReviewRepository reviewRepository,
        ICurrentUserContextService currentUserContextService,
        IUserRepository userRepository
    )
    {
        this.reviewRepository = reviewRepository;
        this.userRepository = userRepository;
        this.currentUserContextService = currentUserContextService;
    }

    public async Task<bool> CreateRangeFromEntitiesAsync(List<Review> reviews)
    {

        if(reviews.Any(r => r.Rating < 0 || r.Rating > 5))
        {
            throw new InvalidRatingException();
        }

        await reviewRepository.CreateRangeAsync(reviews);

        var users = await userRepository.GetRangeAsync(
            [.. reviews.Select(r => r.ReviewedUserId)]
        );

        foreach (var user in users)
        {
            var usersReview = reviews.Find(r => r.ReviewedUserId == user.UserId);

            if(usersReview is null)
            {
                continue;
            }

            user.Rating = (user.Rating * user.TotalReviews + usersReview.Rating) / (user.TotalReviews + 1);

            user.TotalReviews += 1;
        }

        await userRepository.UpdateRangeAsync(users);

        return true;
    }
}
