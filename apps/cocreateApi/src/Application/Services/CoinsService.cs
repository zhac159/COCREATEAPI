using Application.Exceptions;
using Application.Interfaces;
using Infrastructure.Interfaces;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Application.Services;

public class CoinsService(ICurrentUser currentUser, CoCreateDbContext context) : ICoinsService
{
    public async Task<bool> CanAfford(int cost)
    {
        var coins = await context
            .Users.Where(up => up.Id == currentUser.GetUserId())
            .Select(up => up.Coins)
            .FirstOrDefaultAsync();

        return coins >= cost;
    }

    public async Task DeductCoins(int amount)
    {
        var user =
            await context.Users.FirstOrDefaultAsync(u => u.Id == currentUser.GetUserId())
            ?? throw new UserNotFoundException("User not found when deducting coins.");

        user.Coins -= amount;

        context.Users.Update(user);
        await context.SaveChangesAsync();
    }
}
