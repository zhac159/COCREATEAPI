using Application.Exceptions;
using Application.Features.AuthenticationFeature.Common;
using Application.Features.AuthenticationFeature.GetJwtToken;
using FluentValidation;
using Infrastructure.Interfaces;
using Infrastructure.Persistence;
using Mediator;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.AuthenticationFeature.TokenLogin;

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
            await coCreateDbContext.Users.FirstOrDefaultAsync(u => u.Id == currentUser.GetUserId())
            ?? throw new UserNotFoundException("User not found for token login");

        var jwtToken = await mediator.Send(
            new GetJwtTokenRequest() { Email = user.Email, UserId = user.Id },
            cancellationToken
        );

        return new LoginResponse()
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
