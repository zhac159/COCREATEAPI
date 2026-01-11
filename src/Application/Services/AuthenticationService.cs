using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Application.DTOs.UserDtos;
using Application.Interfaces;
using Domain.Exceptions;
using Microsoft.IdentityModel.Tokens;

namespace Application.Services;

public class AuthenticationService : IAuthenticationService
{
    private readonly IConfiguration config;

    public AuthenticationService(IConfiguration config)
    {
        this.config = config;
    }

    public string CreateJWTTokenAsync(UserLoginResponseDTO user)
    {
        var claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.Sub, user.UserId.ToString()),
            new Claim(JwtRegisteredClaimNames.Email, user.Email)
        };

        var securityKey = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(config["Jwt:Key"] ?? throw new InvalidOperationException())
        );
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        var Sectoken = new JwtSecurityToken(
            config["Jwt:Issuer"],
            config["Jwt:Issuer"],
            claims: claims,
            expires: DateTime.Now.AddMinutes(120),
            signingCredentials: credentials
        );

        var token = new JwtSecurityTokenHandler().WriteToken(Sectoken);

        return token;
    }

    public int AuthenticateJWTTokenAndGetUserId(string token)
    {
        try
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var securityKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(config["Jwt:Key"] ?? throw new InvalidOperationException())
            );

            tokenHandler.ValidateToken(
                token,
                new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = config["Jwt:Issuer"],
                    ValidAudience = config["Jwt:Issuer"],
                    IssuerSigningKey = securityKey
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
        catch
        {
            throw new TokenExpiredException();
        }
    }
}
