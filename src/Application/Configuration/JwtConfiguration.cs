namespace Application.Configuration;

public sealed class JwtOptions
{
    public const string SectionName = "Jwt";
    public required string Key { get; init; }
    public required string Issuer { get; init; }
    public required int ExpiryDays { get; init; }
}
