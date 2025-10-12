namespace Application.Features.Authentication.Common;

public class AuthenticatedUser
{
    public required int UserId { get; set; }
    public required string Username { get; set; }
    public required string Email { get; set; }
    public required int Coins { get; set; }
}

public class LoginResponse
{
    public required string Token { get; set; }
    public required AuthenticatedUser User { get; set; }
}
