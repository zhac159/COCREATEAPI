using NetTopologySuite.Geometries;

namespace Infrastructure.Entities;

public class User
{
    public int Id { get; set; }
    public required string Username { get; set; }
    public required string PasswordHash { get; set; }
    public required string Email { get; set; }
    public bool IsEmailVerified { get; set; } = false;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public string? Address { get; set; }
    public Point? Location { get; set; }
    public required int Coins { get; set; } = 0;
    public string? AboutYou { get; set; }
    public string? ProfilePictureSrc { get; set; }
    public string? BannerPictureSrc { get; set; }
    public List<Skill> Skills { get; set; } = [];
    public List<PortfolioContentMedia> PortfolioMedias { get; set; } = [];
    public List<Project> Projects { get; set; } = [];
    public List<ProjectRole> ProjectRoles { get; set; } = [];
}
