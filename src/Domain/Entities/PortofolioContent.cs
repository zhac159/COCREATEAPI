using Domain.Enums;

namespace Domain.Entities;

public class PortofolioContent
{
    public int Id { get; set; }
    public required string Description { get; set; }
    public required SkillType SkillType { get; set; }
    public required DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public required int Order { get; set; }
    public int UserId { get; set; }
    public User? User { get; set; }
    public List<PortofolioContentMedia> Medias { get; set; } = new List<PortofolioContentMedia>();
}
