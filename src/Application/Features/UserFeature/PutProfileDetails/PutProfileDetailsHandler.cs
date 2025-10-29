using Application.Exceptions;
using Application.Features.UserFeature.Common;
using Application.Interfaces;
using FluentValidation;
using Infrastructure.Entities;
using Infrastructure.Persistence;
using Mediator;

namespace Application.Features.UserFeature.PutProfileDetails;

public sealed record UpdateProfileDetailsRequest : ICommand<ProfileDetails>
{
    public required UpdateProfileDetails ProfileDetails { get; set; }
}

public sealed class UpdateProfileDetailsRequestValidator
    : AbstractValidator<UpdateProfileDetailsRequest>
{
    public UpdateProfileDetailsRequestValidator()
    {
        RuleFor(x => x).NotNull();
    }
}

public sealed class UpdateProfileDetailsRequestHandler(
    CoCreateDbContext coCreateDbContext,
    ICurrentUser currentUser
) : ICommandHandler<UpdateProfileDetailsRequest, ProfileDetails>
{
    public async ValueTask<ProfileDetails> Handle(
        UpdateProfileDetailsRequest command,
        CancellationToken cancellationToken
    )
    {
        var user =
            await coCreateDbContext.Users.FindAsync([currentUser.GetUserId()], cancellationToken)
            ?? throw new UserNotFoundException("User not found.");

        user.Username = command.ProfileDetails.Username;
        user.Email = command.ProfileDetails.Email;
        user.AboutYou = command.ProfileDetails.AboutYou;
        user.Location = command.ProfileDetails.Location?.ToPoint();
        user.ProfilePictureSrc = command.ProfileDetails.ProfilePicture.Uri;

        user.Skills.RemoveAll(s => !command.ProfileDetails.Skills.Any(pds => pds.Id == s.Id));
        foreach (var skill in command.ProfileDetails.Skills)
        {
            var existingSkill = user.Skills.FirstOrDefault(s => s.Id == skill.Id);
            if (existingSkill != null)
            {
                existingSkill.SkillType = skill.SkillType;
                existingSkill.SkillGroupType = skill.SkillGroupType;
                existingSkill.Keywords = skill.Keywords;
            }
            else
            {
                user.Skills.Add(
                    new Skill
                    {
                        SkillType = skill.SkillType,
                        SkillGroupType = skill.SkillGroupType,
                        Keywords = skill.Keywords,
                        UserId = user.Id,
                    }
                );
            }
        }

        user.PortfolioMedias.RemoveAll(pm =>
            !command.ProfileDetails.PortfolioMedias.Any(pdm => pdm.Id == pm.Id)
        );
        foreach (var media in command.ProfileDetails.PortfolioMedias)
        {
            var existingMedia = user.PortfolioMedias.FirstOrDefault(pm => pm.Id == media.Id);
            if (existingMedia != null)
            {
                existingMedia.MediaType = media.MediaType;
                existingMedia.Uri = media.Uri;
            }
            else
            {
                user.PortfolioMedias.Add(
                    new PortflioContentMedia
                    {
                        MediaType = media.MediaType,
                        Uri = media.Uri,
                        UserId = user.Id,
                        Order = media.Order,
                    }
                );
            }
        }

        await coCreateDbContext.SaveChangesAsync(cancellationToken);

        return ProfileDetails.FromUser(user);
    }
}
