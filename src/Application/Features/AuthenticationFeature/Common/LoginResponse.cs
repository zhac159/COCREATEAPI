namespace Application.Features.AuthenticationFeature.Common;

public record LoginResponse
{
    public required string Token { get; set; }
    public required AuthenticatedUser User { get; set; }
}
