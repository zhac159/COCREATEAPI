using Application.Exceptions;
using Application.Features.Authentication.Common;
using Application.Features.Authentication.GetJwtToken;
using Application.Interfaces;
using FluentValidation;
using Infrastructure.Persistence;
using Mediator;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.Authentication.TokenLogin;

public sealed record TokenRequest : ICommand<LoginResponse>;

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
    IMediator mediator
) : ICommandHandler<TokenRequest, LoginResponse>
{
    public async ValueTask<LoginResponse> Handle(
        TokenRequest command,
        CancellationToken cancellationToken
    )
    {
        var user =
            await coCreateDbContext.Users.FirstOrDefaultAsync(u =>
                u.UserId == currentUser.GetUserId()
            ) ?? throw new UserNotFoundException("User not found for token login");

        var jwtToken = await mediator.Send(
            new GetJwtTokenRequest() { Email = user.Email, UserId = user.UserId },
            cancellationToken
        );

        return new LoginResponse()
        {
            Token = jwtToken.AccessToken,
            User = new AuthenticatedUser
            {
                UserId = user.UserId,
                Username = user.Username,
                Email = user.Email,
                Coins = user.Coins,
            },
        };
    }
}
