using System.ComponentModel.DataAnnotations;
using Domain.Enums;

namespace Domain.Entities;

public class Skill
{
    public int Id { get; set; }
    public required SkillType SkillType { get; set; }
    public string? Description { get; set; }
    public required int Level { get; set; }
    public int UserId { get; set; }
    public User? User { get; set; }
}