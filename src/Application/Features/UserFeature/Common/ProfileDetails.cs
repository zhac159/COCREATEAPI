using Application.Features.Common.Records;
using Infrastructure.Entities;
using Infrastructure.Enums;

namespace Application.Features.UserFeature.Common;

public record ProfileDetails
{
    public required string Username { get; set; }

    public required string Email { get; set; }

    public string? AboutYou { get; set; } = "";

    public required LocationRecord? Location { get; set; }

    public required MediaRecord ProfilePicture { get; set; }

    public List<SkillRecord> Skills { get; set; } = [];

    public List<MediaRecord> PortfolioMedias { get; set; } = [];

    public static ProfileDetails FromUser(User user) =>
        new()
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
                    .Select(pm => new MediaRecord
                    {
                        Id = pm.Id,
                        Uri = pm.Uri,
                        MediaType = pm.MediaType,
                    }),
            ],
            ProfilePicture = MediaRecord.FromUri(user.ProfilePictureSrc ?? "", MediaType.Image),
        };
}

public record UpdateProfileDetails : ProfileDetails
{
    public new required List<UpdateSkillRecord> Skills { get; set; } = [];
    public new required List<UpdateMediaRecord> PortfolioMedias { get; set; } = [];
}
