using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Application.Configuration;
using FluentValidation;
using Mediator;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace Application.Features.Authentication.GetJwtToken;

public sealed record GetJwtTokenRequest : IQuery<GetJwtTokenResponse>
{
    public required int UserId { get; init; }
    public required string Email { get; init; }
}

public sealed class GetJwtTokenRequestValidator : AbstractValidator<GetJwtTokenRequest>
{
    public GetJwtTokenRequestValidator()
    {
        RuleFor(x => x).NotNull();
    }
}

public sealed class GetJwtTokenRequestHandler(IOptionsSnapshot<JwtOptions> jwtOptions)
    : IQueryHandler<GetJwtTokenRequest, GetJwtTokenResponse>
{
    public ValueTask<GetJwtTokenResponse> Handle(
        GetJwtTokenRequest query,
        CancellationToken cancellationToken
    )
    {
        var options = jwtOptions.Value;

        // Create signing key with proper padding for HMAC (consistent with validation)
        var keyBytes = Encoding.UTF8.GetBytes(options.Key);

        // Ensure key is long enough for HMAC256 (minimum 32 bytes) as per RFC2104
        if (keyBytes.Length < 32)
        {
            Array.Resize(ref keyBytes, 32); // Pad with zeros to 32 bytes
        }

        var securityKey = new SymmetricSecurityKey(keyBytes);
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        var claims = new[] { new Claim(JwtRegisteredClaimNames.Sub, query.UserId.ToString()) };

        var expiresAt = DateTime.UtcNow.AddDays(options.ExpiryDays);

        var token = new JwtSecurityToken(
            issuer: options.Issuer,
            audience: options.Issuer,
            claims: claims,
            expires: expiresAt,
            signingCredentials: credentials
        );

        var accessToken = new JwtSecurityTokenHandler().WriteToken(token);

        var response = new GetJwtTokenResponse
        {
            AccessToken = accessToken,
            TokenType = "Bearer",
            ExpiresAt = expiresAt,
        };

        return ValueTask.FromResult(response);
    }
}
