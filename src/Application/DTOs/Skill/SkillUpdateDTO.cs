using Domain.Entities;
using Domain.Enums;

namespace Application.DTOs.SkillDTOs;

public class SkillUpdateDTO
{
    public int? Id { get; set; }
    public required SkillType SkillType { get; set; }
    public string? Description { get; set; }
    public required int Level { get; set; }
    

    public Skill ToEntity()
    {
        return new Skill
        {
            SkillType = SkillType,
            Description = Description,
            Level = Level
        };
    }   
}
