using Application.Exceptions;
using Application.Features.Common.Records;
using Application.Features.UserFeature.Common;
using Application.Interfaces;
using FluentValidation;
using Infrastructure.Enums;
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

        return new ProfileDetails
        {
            Username = user.Username,
            Email = user.Email,
            AboutYou = user.AboutYou,
            Location = LocationRecord.FromUser(user),
            Skills = [.. user.Skills.Select(SkillRecord.FromSkill)],
            PortfolioMedias =
            [
                .. user
                    .PortfolioMedias.OrderBy(m => m.Order)
                    .Select(MediaRecord.FromPorfolioContentMedia),
            ],
            ProfilePicture = MediaRecord.FromUri(user.ProfilePictureSrc ?? "", MediaType.Image),
        };
    }
}
