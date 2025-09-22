using Application.Exceptions;
using Application.Features.Authentication.Common;
using Application.Features.Authentication.GetJwtToken;
using FluentValidation;
using Infrastructure.Persistence;
using Mediator;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.Authentication.Login;

public sealed record LoginRequest : IQuery<LoginResponse>
{
    public required string UsernameOrEmail { get; init; }
    public required string Password { get; init; }
}

public sealed class LoginRequestValidator : AbstractValidator<LoginRequest>
{
    public LoginRequestValidator()
    {
        RuleFor(x => x.Password).NotNull();
        RuleFor(x => x.UsernameOrEmail).NotNull();
    }
}

public sealed class LoginRequestHandler(CoCreateDbContext coCreateDbContext, IMediator mediator)
    : IQueryHandler<LoginRequest, LoginResponse>
{
    public async ValueTask<LoginResponse> Handle(
        LoginRequest query,
        CancellationToken cancellationToken
    )
    {
        var user =
            await coCreateDbContext.Users.FirstOrDefaultAsync(u =>
                (u.Username == query.UsernameOrEmail || u.Email == query.UsernameOrEmail)
                && u.PasswordHash == query.Password
            ) ?? throw new InvalidPasswordException("authentication failed");

        var jwtToken = await mediator.Send(
            new GetJwtTokenRequest() { Email = user.Email, UserId = user.UserId }
        );

        return new()
        {
            Coins = user.Coins,
            Email = user.Email,
            UserId = user.UserId,
            Username = user.Username,
            Token = jwtToken.AccessToken,
        };
    }
}
