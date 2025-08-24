using NetTopologySuite.Geometries;

namespace Domain.Entities;

public class User
{
    public int UserId { get; set; }
    public required string Username { get; set; }
    public required string Password { get; set; }
    public required string Email { get; set; }
    public bool IsEmailVerified { get; set; } = false;
    public string? Address { get; set; }
    public Point? Location { get; set; }
    public double Rating { get; set; } = 0;
    public int TotalReviews { get; set; } = 0;
    public string? AboutYou { get; set; }
    public int Coins { get; set; } = 0;
    public string? ProfilePictureSrc { get; set; }
    public string? BannerPictureSrc { get; set; }
    public string? PublicKey { get; set; }
    public List<SeenMatches> SeenMatches { get; set; } = [];
    public List<Skill> Skills { get; set; } = [];
    public List<PortfolioContentMedia> PortfolioMedias { get; set; } = [];
    public List<Review> ReviewsGiven { get; set; } = [];
    public List<Review> ReviewsReceived { get; set; } = [];
    public List<Asset> Assets { get; set; } = [];
    public List<Project> Projects { get; set; } = [];
    public List<ProjectRole> ProjectRoles { get; set; } = [];
    public List<Enquiry> Enquiries { get; set; } = [];
    public List<Enquiry> EnquiriesReceived { get; set; } = [];
    public List<Experience> Experiences { get; set; } = [];
    public List<ChatMembership> ChatMemberships { get; set; } = [];
}
