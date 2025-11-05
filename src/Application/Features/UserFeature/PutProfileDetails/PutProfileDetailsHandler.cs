using Application.Exceptions;
using Application.Features.UserFeature.Common;
using Application.Interfaces;
using FluentValidation;
using Infrastructure.Entities;
using Infrastructure.Persistence;
using Mediator;
using Microsoft.EntityFrameworkCore;

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
            await coCreateDbContext
                .Users.Include(u => u.Skills)
                .Include(u => u.PortfolioMedias)
                .FirstOrDefaultAsync(u => u.Id == currentUser.GetUserId(), cancellationToken)
            ?? throw new UserNotFoundException("User not found.");

        user.Username = command.ProfileDetails.Username;
        user.Email = command.ProfileDetails.Email;
        user.AboutYou = command.ProfileDetails.AboutYou;
        user.Location = command.ProfileDetails.Location?.ToPoint();
        user.Address = command.ProfileDetails.Location?.Address;
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

        user.PortfolioMedias.RemoveAll(m =>
            !command.ProfileDetails.PortfolioMedias.Any(pdm => pdm.Id == m.Id)
        );

        for (var index = 0; index < command.ProfileDetails.PortfolioMedias.Count; index++)
        {
            var media = command.ProfileDetails.PortfolioMedias[index];
            var existingMedia = user.PortfolioMedias.FirstOrDefault(pm => pm.Id == media.Id);
            if (existingMedia != null)
            {
                existingMedia.MediaType = media.MediaType;
                existingMedia.Uri = media.Uri;
                existingMedia.Order = index;
            }
            else
            {
                user.PortfolioMedias.Add(
                    new PortfolioContentMedia
                    {
                        MediaType = media.MediaType,
                        Uri = media.Uri,
                        UserId = user.Id,
                        Order = index,
                    }
                );
            }
        }

        coCreateDbContext.Users.Update(user);
        await coCreateDbContext.SaveChangesAsync();
        return ProfileDetails.FromUser(user);
    }
}
