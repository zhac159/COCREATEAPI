using Application.Interfaces;
using Domain.Entities;
using Domain.Exceptions;
using Domain.Interfaces;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class UserRepository : IUserRepository
{
    private readonly CoCreateDbContext context;
    private readonly IStorageService storageService;

    public UserRepository(CoCreateDbContext context, IStorageService storageService)
    {
        this.context = context;
        this.storageService = storageService;
    }

    public async Task<User?> GetByUsernameAsync(string name)
    {
        var user = await context
            .Users.Where(u => u.Username == name)
            .Include(u => u.PortofolioContents)
            .Include(u => u.Skills)
            .Include(u => u.ReviewsGiven)
            .Include(u => u.ReviewsReceived)
            .Include(u => u.Assets)
            .ThenInclude(a => a.Medias)
            .FirstOrDefaultAsync();

        PopulateUris(user);

        return user;
    }

    public async Task<User> CreateAsync(User user)
    {
        await context.Users.AddAsync(user);
        await context.SaveChangesAsync();

        return user;
    }

    public async Task<bool> ExistsByNameAsync(string name)
    {
        var user = await context.Users.Where(u => u.Username == name).FirstOrDefaultAsync();

        return user != null;
    }

    public async Task<User?> GetByIdAsync(int id)
    {
        var user = await context.Users.Where(u => u.UserId == id).FirstOrDefaultAsync();

        PopulateUris(user);

        return user;
    }

    public async Task<User?> GetByIdIncludeAllPropertiesAsync(int id)
    {
        var user = await context
            .Users.Where(u => u.UserId == id)
            .Include(u => u.PortofolioContents)
            .Include(u => u.Skills)
            .Include(u => u.ReviewsGiven)
            .Include(u => u.ReviewsReceived)
            .Include(u => u.Assets)
            .ThenInclude(a => a.Medias)
            .FirstOrDefaultAsync();

        PopulateUris(user);

        return user;
    }

    public async Task<User?> GetByIdIncludeSkillsAsync(int id)
    {
        var user = await context
            .Users.Where(u => u.UserId == id)
            .Include(u => u.Skills)
            .FirstOrDefaultAsync();

        return user;
    }

    public async Task<User> UpdateAsync(User user)
    {
        context.Users.Update(user);
        await context.SaveChangesAsync();

        return user;
    }

    private void PopulateUris(User? user)
    {
        if (user?.PortofolioContents != null)
        {
            foreach (var portofolioContent in user.PortofolioContents)
            {
                portofolioContent.Uri = storageService.GetFileUri(
                    portofolioContent.FileSrc,
                    "portofoliocontents"
                );
            }
        }
    }

    public async Task<int?> GetCoinByIdAsync(int id)
    {
        var user = await context.Users.Where(u => u.UserId == id).FirstOrDefaultAsync();

        return user?.Coins;
    }

    public async Task<int?> UpdateCoinByIdAsync(int id, int coin)
    {
        var user = await context.Users.Where(u => u.UserId == id).FirstOrDefaultAsync();

        if (user is null)
        {
            return null;
        }

        user.Coins = coin;

        await context.SaveChangesAsync();

        return user.Coins;
    }
}
