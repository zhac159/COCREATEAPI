using Application.Features.Common.Records;

namespace Application.Features.UserFeature.GetProfileDetails;

public record GetProfileDetailsResponse
{
    public required string Username { get; set; }

    public required string Email { get; set; }

    public string? AboutYou { get; set; } = "";

    public required LocationRecord? Location { get; set; }

    public required MediaRecord ProfilePicture { get; set; }

    public List<SkillRecord> Skills { get; set; } = [];

    public List<MediaRecord> PortfolioMedias { get; set; } = [];
}
