namespace Domain.Entities;

public class Review
{
    public int Id { get; set; }
    public required string Description { get; set; }
    public required int Rating { get; set; }
    public required int ReviewerUserId { get; set; }
    public required User ReviewerUser { get; set; }
    public required int ReviewedUserId { get; set; }
    public required User ReviewedUser { get; set; }
}
