using Infrastructure.Entities;
using Infrastructure.Enums;

namespace Application.Features.Common.Records;

public abstract record SkillRecordBase
{
    public required SkillType SkillType { get; set; }
    public required SkillGroupType SkillGroupType { get; set; }
    public List<string> Keywords { get; set; } = [];
}

public record SkillRecord : SkillRecordBase
{
    public int Id { get; set; }

    public static SkillRecord FromSkill(Skill skill) =>
        new()
        {
            Id = skill.Id,
            SkillType = skill.SkillType,
            SkillGroupType = skill.SkillGroupType,
            Keywords = skill.Keywords ?? [],
        };
}

public record UpdateSkillRecord : SkillRecordBase
{
    public int? Id { get; set; }
}
