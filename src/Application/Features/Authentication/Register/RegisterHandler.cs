using Application.Exceptions;
using Application.Features.Authentication.Common;
using Application.Features.Authentication.GetJwtToken;
using FluentValidation;
using Infrastructure.Entities;
using Infrastructure.Persistence;
using Mediator;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.Authentication.Register;

public sealed record RegisterRequest : ICommand<LoginResponse>
{
    public required string Username { get; init; }
    public required string Email { get; init; }
    public required string Password { get; init; }
}

public sealed class RegisterRequestValidator : AbstractValidator<RegisterRequest>
{
    public RegisterRequestValidator()
    {
        RuleFor(x => x).NotNull();
    }
}

public sealed class RegisterRequestHandler(CoCreateDbContext coCreateDbContext, IMediator mediator)
    : ICommandHandler<RegisterRequest, LoginResponse>
{
    public async ValueTask<LoginResponse> Handle(
        RegisterRequest command,
        CancellationToken cancellationToken
    )
    {
        var existingUser = await coCreateDbContext.Users.FirstOrDefaultAsync(
            u => u.Email == command.Email || u.Username == command.Username,
            cancellationToken
        );

        if (existingUser is not null)
        {
            if (existingUser.Email == command.Email)
            {
                throw new EmailTakenException("Email is already in use.");
            }
            if (existingUser.Username == command.Username)
            {
                throw new UsernameTakenException("Username is already in use.");
            }
        }

        var user = new User
        {
            Username = command.Username,
            Email = command.Email,
            PasswordHash = command.Password,
            Coins = 0,
        };

        coCreateDbContext.Users.Add(user);
        await coCreateDbContext.SaveChangesAsync(cancellationToken);

        var jwtToken = await mediator.Send(
            new GetJwtTokenRequest() { Email = user.Email, UserId = user.UserId },
            cancellationToken
        );

        return new LoginResponse
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
