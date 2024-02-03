using Domain.Enums;

namespace Application.DTOs.SkillDTOs;

public class SkillDTO
{
    public int Id { get; set; }
    public required SkillType SkillType { get; set; }
    public string? Description { get; set; }
    public required int Level { get; set; }
}
