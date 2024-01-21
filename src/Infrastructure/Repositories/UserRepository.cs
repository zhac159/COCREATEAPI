using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class UserRepository : IUserRepository
{
    private readonly CoCreateDbContext context;

    public UserRepository(CoCreateDbContext context)
    {
        this.context = context;
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
            .FirstOrDefaultAsync();

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
        var user = await context
            .Users.Where(u => u.UserId == id)
            .Include(u => u.PortofolioContents)
            .Include(u => u.Skills)
            .Include(u => u.ReviewsGiven)
            .Include(u => u.ReviewsReceived)
            .Include(u => u.Assets)
            .FirstOrDefaultAsync();

        return user;
    }

    public async Task<User> UpdateAsync(User user)
    {
        context.Users.Update(user);
        await context.SaveChangesAsync();

        return user;
    }
}
