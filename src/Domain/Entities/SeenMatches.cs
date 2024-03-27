namespace Domain.Entities;

public class SeenMatches
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public User? User { get; set; }
    public int ProjectRoleId { get; set; }
    public ProjectRole? ProjectRole { get; set; }
    public DateTime SeenAt { get; set; }
}