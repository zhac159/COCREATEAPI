using Application.Features.Common.Records;

namespace Application.Features.UserFeature.Common;

public record ProfileDetailsBase
{
    public required string Username { get; set; }

    public required string Email { get; set; }

    public string? AboutYou { get; set; } = "";

    public required LocationRecord? Location { get; set; }

    public required MediaRecord ProfilePicture { get; set; }

    public List<SkillRecord> Skills { get; set; } = [];

    public List<MediaRecord> PortfolioMedias { get; set; } = [];
}

public record ProfileDetails : ProfileDetailsBase;

public record UpdateProfileDetails : ProfileDetailsBase
{
    public new List<UpdateSkillRecord> Skills { get; set; } = [];
    public new List<UpdateMediaRecord> PortfolioMedias { get; set; } = [];
}
