using System.ComponentModel.DataAnnotations;
using Domain.Enums;

namespace Domain.Entities;

public class Skill
{
    public int Id { get; set; }
    public required SkillType SkillType { get; set; }
    public required string Description { get; set; } = "";

    [Range(1, 10)]
    public required int Level { get; set; }
    
    public required int UserId { get; set; }
    public required User User { get; set; }
}