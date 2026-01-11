using System.ComponentModel.DataAnnotations;
using Domain.Entities;
using Domain.Enums;

namespace Application.DTOs.SkillDTOs;

public class SkillUpdateDTO
{
    public int? Id { get; set; }

    [Required]
    public required SkillType SkillType { get; set; }

    [Required]
    public required SkillGroupType SkillGroupType { get; set; }
    public string? Description { get; set; }
    public int Level { get; set; }
    public List<string> Keywords { get; set; } = [];

    public Skill ToEntity()
    {
        return new Skill
        {
            SkillType = SkillType,
            SkillGroupType = SkillGroupType,
            Description = Description,
            Level = Level,
            Keywords = Keywords
        };
    }
}
