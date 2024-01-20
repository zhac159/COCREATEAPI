using Application.DTOs.SkillDTOs;
using Domain.Entities;

namespace Application.Extensions;

public static class SkillExtensions
{
    public static void UpdateFromDTO(this Skill skill, SkillUpdateDTO skillUpdateDTO)
    {
        skill.SkillType = skillUpdateDTO.SkillType;
        skill.Description = skillUpdateDTO.Description;
        skill.Level = skillUpdateDTO.Level;
    }

    public static SkillDTO ToDTO(this Skill skill)
    {
        return new SkillDTO
        {
            Id = skill.Id,
            SkillType = skill.SkillType,
            Description = skill.Description,
            Level = skill.Level
        };
    }
}
