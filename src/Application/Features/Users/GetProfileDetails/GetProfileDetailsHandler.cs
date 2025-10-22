using Application.Exceptions;
using Application.Features.Common.Records;
using Application.Interfaces;
using FluentValidation;
using Infrastructure.Enums;
using Infrastructure.Persistence;
using Mediator;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.Users.GetProfileDetails;

public sealed record GetProfileDetailsRequest : IQuery<GetProfileDetailsResponse>;

public sealed class GetProfileDetailsRequestHandler(
    CoCreateDbContext coCreateDbContext,
    ICurrentUser currentUser
) : IQueryHandler<GetProfileDetailsRequest, GetProfileDetailsResponse>
{
    public async ValueTask<GetProfileDetailsResponse> Handle(
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

        return new GetProfileDetailsResponse
        {
            Username = user.Username,
            Email = user.Email,
            AboutYou = user.AboutYou,
            Location = LocationRecord.FromUser(user),
            Skills = [.. user.Skills.Select(SkillRecord.FromSkill)],
            PortfolioMedias =
            [
                .. user.PortfolioMedias.Select(MediaRecord.FromPorfolioContentMedia),
            ],
            ProfilePicture = MediaRecord.FromUri(user.ProfilePictureSrc ?? "", MediaType.Image),
        };
    }
}
