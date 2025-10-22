namespace Application.Features.Authentication.Common;

public record AuthenticatedUser
{
    public required int UserId { get; set; }
    public required string Username { get; set; }
    public required string Email { get; set; }
    public required int Coins { get; set; }
}
