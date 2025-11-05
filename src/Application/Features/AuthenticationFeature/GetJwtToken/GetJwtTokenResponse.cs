namespace Application.Features.AuthenticationFeature.GetJwtToken;

public record GetJwtTokenResponse
{
    public required string AccessToken { get; init; }
    public required string TokenType { get; init; } = "Bearer";
    public required DateTime ExpiresAt { get; init; }
}
