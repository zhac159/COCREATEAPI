namespace Domain.Entities;

public class ChatMembership
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public User User { get; set; } = null!;
    public int ChatId { get; set; }
    public Chat Chat { get; set; } = null!;
}
