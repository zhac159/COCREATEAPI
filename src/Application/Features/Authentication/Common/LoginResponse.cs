namespace Application.Features.Authentication.Common;

public class LoginResponse
{
    public string Token { get; set; } = string.Empty;
    public required int UserId { get; set; }
    public required string Username { get; set; }
    public required string Email { get; set; }
    public required int Coins { get; set; }
}
