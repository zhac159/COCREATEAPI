using Application.Exceptions;
using Application.Interfaces;
using Infrastructure.Interfaces;
using Infrastructure.Persistence;

namespace Application.Services;

public class CoinsService(ICurrentUser currentUser, CoCreateDbContext context) : ICoinsService
{
    public Task<bool> CanAfford(int cost)
    {
        var coins = context
            .Users.Where(up => up.Id == currentUser.GetUserId())
            .Select(up => up.Coins)
            .FirstOrDefault();

        return Task.FromResult(coins >= cost);
    }

    public Task DeductCoins(int amount)
    {
        var user =
            context.Users.FirstOrDefault(u => u.Id == currentUser.GetUserId())
            ?? throw new UserNotFoundException("User not found when deducting coins.");

        user.Coins -= amount;

        context.Users.Update(user);
        context.SaveChanges();

        return Task.CompletedTask;
    }
}
