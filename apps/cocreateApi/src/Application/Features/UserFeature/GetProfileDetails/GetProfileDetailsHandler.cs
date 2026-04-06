using Application.Exceptions;
using Application.Features.UserFeature.Common;
using FluentValidation;
using Infrastructure.Interfaces;
using Infrastructure.Persistence;
using Mediator;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.UserFeature.GetProfileDetails;

public sealed record GetProfileDetailsRequest : IQuery<ProfileDetails>;

public sealed class GetProfileDetailsRequestHandler(
    CoCreateDbContext coCreateDbContext,
    ICurrentUser currentUser
) : IQueryHandler<GetProfileDetailsRequest, ProfileDetails>
{
    public async ValueTask<ProfileDetails> Handle(
        GetProfileDetailsRequest query,
        CancellationToken cancellationToken
    )
    {
        var user =
            await coCreateDbContext
                .Users.Where(u => u.Id == currentUser.GetUserId())
                .Include(u => u.Skills)
                .Include(u => u.PortfolioMedias)
                .FirstOrDefaultAsync() ?? throw new UserNotFoundException("User not found");

        return ProfileDetails.FromUser(user);
    }
}
