using Infrastructure.Enums;

namespace Infrastructure.Entities;

public class Skill
{
    public int Id { get; set; }
    public required SkillType SkillType { get; set; }
    public required SkillGroupType SkillGroupType { get; set; }
    public List<string>? Keywords { get; set; }
    public int UserId { get; set; }
    public User? User { get; set; }
}
