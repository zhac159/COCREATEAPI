namespace Domain.Entities;

public class Review
{
    public int Id { get; set; }
    public string? Description { get; set; }
    public required double Rating { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public int ReviewerUserId { get; set; }
    public User? ReviewerUser { get; set; }
    public int ReviewedUserId { get; set; }
    public User? ReviewedUser { get; set; }
}
