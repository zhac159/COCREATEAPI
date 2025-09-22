using System.Text;
using API.Configuration;
using API.Filters;
using Application.Configuration;
using Infrastructure.Configuration;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

// Enable PII logging for better JWT debugging (as per Stack Overflow answer)
Microsoft.IdentityModel.Logging.IdentityModelEventSource.ShowPII = true;

Console.WriteLine($"[Jwt] Configuration - Issuer:");

builder.Services.Configure<JwtOptions>(builder.Configuration.GetSection(JwtOptions.SectionName));
builder
    .Services.AddAuthentication(x =>
    {
        x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    })
    .AddJwtBearer(options =>
    {
        Console.WriteLine($"adding jwt");

        var jwtOptions = builder
            .Configuration.GetSection(JwtOptions.SectionName)
            .Get<JwtOptions>()!;

        // Create signing key with proper padding for HMAC (as per Stack Overflow solution)
        var keyBytes = Encoding.UTF8.GetBytes(jwtOptions.Key);

        // Ensure key is long enough for HMAC256 (minimum 32 bytes) as per RFC2104
        if (keyBytes.Length < 32)
        {
            Array.Resize(ref keyBytes, 32); // Pad with zeros to 32 bytes
        }

        var signingKey = new SymmetricSecurityKey(keyBytes);

        options.TokenValidationParameters = new()
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = jwtOptions.Issuer,
            ValidAudience = jwtOptions.Issuer,
            IssuerSigningKey = signingKey,

            // Critical settings for symmetric key without Key ID
            TryAllIssuerSigningKeys = true,
            RequireSignedTokens = true,
            RequireExpirationTime = true,

            // Additional debugging settings
            ClockSkew = TimeSpan.FromMinutes(5),
        };

        Console.WriteLine($"[Jwt] Configuration - Issuer: '{jwtOptions.Issuer}'");
        Console.WriteLine(
            $"[Jwt] Configuration - Key length: {jwtOptions.Key?.Length ?? 0} characters"
        );

        // Helpful debug hooks: write auth failures/validation events to console so we can see
        // why a token is being rejected in runtime (useful during development).
        options.Events = new Microsoft.AspNetCore.Authentication.JwtBearer.JwtBearerEvents
        {
            OnMessageReceived = context =>
            {
                // show whether an Authorization header was present and its prefix
                var auth = context.Request.Headers["Authorization"].ToString();
                Console.WriteLine($"[Jwt] OnMessageReceived - Authorization header: '{auth}'");

                // Extract and analyze the token
                if (!string.IsNullOrEmpty(auth) && auth.StartsWith("Bearer "))
                {
                    var token = auth.Substring("Bearer ".Length);
                    Console.WriteLine($"[Jwt] Token length: {token.Length}");

                    // Split the JWT into its parts
                    var parts = token.Split('.');
                    Console.WriteLine($"[Jwt] Token parts count: {parts.Length}");

                    if (parts.Length >= 1)
                    {
                        Console.WriteLine(
                            $"[Jwt] Header part: '{parts[0]}' (length: {parts[0].Length})"
                        );
                        // Try to decode the header to see what's wrong
                        try
                        {
                            var headerBytes = Convert.FromBase64String(
                                AddPaddingIfNeeded(parts[0])
                            );
                            var headerJson = Encoding.UTF8.GetString(headerBytes);
                            Console.WriteLine($"[Jwt] Decoded header: {headerJson}");
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine($"[Jwt] Failed to decode header: {ex.Message}");
                        }
                    }

                    if (parts.Length >= 2)
                    {
                        Console.WriteLine(
                            $"[Jwt] Payload part: '{parts[1]}' (length: {parts[1].Length})"
                        );
                        // Try to decode the payload
                        try
                        {
                            var payloadBytes = Convert.FromBase64String(
                                AddPaddingIfNeeded(parts[1])
                            );
                            var payloadJson = Encoding.UTF8.GetString(payloadBytes);
                            Console.WriteLine($"[Jwt] Decoded payload: {payloadJson}");
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine($"[Jwt] Failed to decode payload: {ex.Message}");
                        }
                    }

                    if (parts.Length >= 3)
                    {
                        Console.WriteLine(
                            $"[Jwt] Signature part: '{parts[2]}' (length: {parts[2].Length})"
                        );
                    }
                }

                return Task.CompletedTask;
            },
            OnTokenValidated = context =>
            {
                Console.WriteLine(
                    $"[Jwt] OnTokenValidated - Principal: {context.Principal?.Identity?.Name}"
                );
                return Task.CompletedTask;
            },
            OnAuthenticationFailed = context =>
            {
                Console.WriteLine(
                    $"[Jwt] OnAuthenticationFailed - Exception: {context.Exception?.GetType().FullName}: {context.Exception?.Message}"
                );
                if (context.Exception?.InnerException != null)
                {
                    Console.WriteLine(
                        $"[Jwt] Inner Exception: {context.Exception.InnerException.GetType().FullName}: {context.Exception.InnerException.Message}"
                    );
                }
                return Task.CompletedTask;
            },
        };

        // Helper method for Base64 padding
        static string AddPaddingIfNeeded(string base64)
        {
            switch (base64.Length % 4)
            {
                case 2:
                    return base64 + "==";
                case 3:
                    return base64 + "=";
                default:
                    return base64;
            }
        }
    });

builder.Services.AddHttpContextAccessor();

builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
builder.Services.AddProblemDetails();

builder.Services.AddOpenApi(options =>
{
    options.AddDocumentTransformer(
        (document, context, cancellationToken) =>
        {
            document.Components ??= new OpenApiComponents();
            document.Components.SecuritySchemes["Bearer"] = new OpenApiSecurityScheme
            {
                Type = SecuritySchemeType.Http,
                Scheme = "bearer",
                BearerFormat = "JWT",
                Description = "Enter your JWT token in the format: Bearer {your token}",
            };

            document.SecurityRequirements.Add(
                new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer",
                            },
                        },
                        Array.Empty<string>()
                    },
                }
            );

            return Task.CompletedTask;
        }
    );
});

builder.Services.AddAuthorization();

builder
    .Services.AddApiServices()
    .AddApplicationServices(builder.Configuration)
    .AddInfrastructureServices(builder.Configuration);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
}

app.UseAuthentication();
app.UseAuthorization();

app.UseExceptionHandler();

app.MapGroup("/api").MapFeatureEndpoints().RequireAuthorization();

app.Run();

public partial class Program { }
