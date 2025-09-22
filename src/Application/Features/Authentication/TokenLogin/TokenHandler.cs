using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Application.Configuration;
using Application.Exceptions;
using Application.Features.Authentication.Common;
using Application.Features.Authentication.GetJwtToken;
using Application.Interfaces;
using FluentValidation;
using Infrastructure.Persistence;
using Mediator;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace Application.Features.Authentication.TokenLogin;

public sealed record TokenRequest : ICommand<LoginResponse>
{
    public string Token { get; init; } = string.Empty;
}

public sealed class TokenRequestValidator : AbstractValidator<TokenRequest>
{
    public TokenRequestValidator()
    {
        RuleFor(x => x).NotNull();
    }
}

public sealed class TokenRequestHandler(
    CoCreateDbContext coCreateDbContext,
    ICurrentUser currentUser,
    IMediator mediator,
    IOptionsSnapshot<JwtOptions> jwtOptions
) : ICommandHandler<TokenRequest, LoginResponse>
{
    public async ValueTask<LoginResponse> Handle(
        TokenRequest command,
        CancellationToken cancellationToken
    )
    {
        var userId = AuthenticateJWTTokenAndGetUserId(command.Token);

        var user =
            await coCreateDbContext.Users.FirstOrDefaultAsync(
                u => u.UserId == userId,
                cancellationToken
            ) ?? throw new UserNotFoundException("User not found for token login");

        var jwtToken = await mediator.Send(
            new GetJwtTokenRequest() { Email = user.Email, UserId = user.UserId },
            cancellationToken
        );

        return new LoginResponse()
        {
            UserId = user.UserId,
            Username = user.Username,
            Email = user.Email,
            Coins = user.Coins,
            Token = jwtToken.AccessToken,
        };
    }

    public int AuthenticateJWTTokenAndGetUserId(string token)
    {
        try
        {
            if (string.IsNullOrWhiteSpace(token))
            {
                throw new ArgumentException("token is null or empty");
            }

            // Accept either the raw token or the full Authorization header value
            // (e.g. "Bearer eyJ..."), which some callers mistakenly pass through.
            token = token.Trim();
            if (token.StartsWith("Bearer ", StringComparison.OrdinalIgnoreCase))
            {
                token = token.Substring(7).Trim();
            }
            // Remove surrounding quotes if present
            if (
                (token.StartsWith("\"") && token.EndsWith("\""))
                || (token.StartsWith("'") && token.EndsWith("'"))
            )
            {
                token = token[1..^1];
            }

            var tokenHandler = new JwtSecurityTokenHandler();
            var securityKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(jwtOptions.Value.Key)
            );

            tokenHandler.ValidateToken(
                token,
                new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    // Match the API middleware which does not validate audience by default.
                    ValidateAudience = false,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = jwtOptions.Value.Issuer,
                    IssuerSigningKey = securityKey,
                },
                out SecurityToken validatedToken
            );

            var jwtToken = (JwtSecurityToken)validatedToken;

            var subClaim =
                jwtToken.Claims.FirstOrDefault(c => c.Type == JwtRegisteredClaimNames.Sub)
                ?? throw new Exception("Invalid token");

            var userId = int.Parse(subClaim.Value);

            return userId;
        }
        catch (Exception ex)
        {
            // Preserve the original exception for diagnostics.
            throw new Exception("Failed to validate JWT token", ex);
        }
    }
}
