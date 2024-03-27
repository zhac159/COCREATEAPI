using Domain.Enums;

namespace Application.DTOs.SkillDTOs;

public class SkillDTO
{
    public int Id { get; set; }
    public required SkillType SkillType { get; set; }
    public required SkillGroupType SkillGroupType { get; set; }
    public string? Description { get; set; }
    public int Level { get; set; }
}
