using Application.Exceptions;
using Application.Features.AuthenticationFeature.Common;
using Application.Features.AuthenticationFeature.GetJwtToken;
using FluentValidation;
using Infrastructure.Persistence;
using Mediator;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.AuthenticationFeature.Login;

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
                u.Username == query.UsernameOrEmail || u.Email == query.UsernameOrEmail
            ) ?? throw new UsernameOrEmailNotFoundException("User or email not found");

        if (user.PasswordHash != query.Password)
        {
            throw new InvalidPasswordException("Invalid password at login");
        }

        var jwtToken = await mediator.Send(
            new GetJwtTokenRequest() { Email = user.Email, UserId = user.Id }
        );

        return new LoginResponse
        {
            Token = jwtToken.AccessToken,
            User = new AuthenticatedUser
            {
                UserId = user.Id,
                Username = user.Username,
                Email = user.Email,
                Coins = user.Coins,
            },
        };
    }
}
