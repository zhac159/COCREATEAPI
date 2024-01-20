namespace Domain.Entities;

public class User
{
    public int UserId { get; set; }
    public required string Username { get; set; }
    public required string Password { get; set; }
    public required string Email { get; set; }
    public required string Location { get; set; }
    public required string AboutYou { get; set; } = "";
    public int Rating { get; set; } = 0;
    public int TotalReviews { get; set; } = 0;
    public string? ProfilePictureSrc { get; set; }
    public string? BannerPictureSrc { get; set; }
    public List<Skill>? Skills { get; set; }
    public List<PortofolioContent>? PortofolioContents { get; set; }
    public List<Review>? ReviewsGiven { get; set; }
    public List<Review>? ReviewsReceived { get; set; }
    public List<Asset>? Assets { get; set; }
}
