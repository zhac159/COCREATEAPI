using NetTopologySuite.Geometries;

namespace Domain.Entities;

public class User
{
    public int UserId { get; set; }
    public required string Username { get; set; }
    public required string Password { get; set; }
    public required string Email { get; set; }
    public string? Address { get; set; }
    public Point? Location { get; set; }
    public int Rating { get; set; } = 0;
    public int TotalReviews { get; set; } = 0;
    public string? AboutYou { get; set; }
    public int Coins { get; set; } = 0;
    public string? ProfilePictureSrc { get; set; }
    public string? BannerPictureSrc { get; set; }
    public List<SeenMatches> SeenMatches { get; set; } = new List<SeenMatches>();
    public List<Skill> Skills { get; set; } = new List<Skill>();
    public List<PortofolioContent> PortofolioContents { get; set; } = new List<PortofolioContent>();
    public List<Review> ReviewsGiven { get; set; } = new List<Review>();
    public List<Review> ReviewsReceived { get; set; } = new List<Review>();
    public List<Asset> Assets { get; set; } = new List<Asset>();
    public List<Project> Projects { get; set; } = new List<Project>();
    public List<ProjectRole> ProjectRoles { get; set; } = new List<ProjectRole>();
    public List<Enquiry> Enquiries { get; set; } = new List<Enquiry>();
    public List<Enquiry> EnquiriesReceived { get; set; } = new List<Enquiry>();
}
