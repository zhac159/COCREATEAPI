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
            .ThenInclude(pc => pc.Medias)
            .Include(u => u.Skills)
            .Include(u => u.ReviewsGiven)
            .Include(u => u.ReviewsReceived)
            .Include(u => u.Assets)
            .ThenInclude(a => a.Medias)
            .Include(u => u.Projects)
            .ThenInclude(p => p.Medias)
            .Include(u => u.Projects)
            .ThenInclude(p => p.ProjectRoles)
            .ThenInclude(pr => pr.Medias)
            .Include(u => u.Projects)
            .ThenInclude(p => p.ProjectRoles)
            .ThenInclude(pr => pr.Enquiries)
            .ThenInclude(e => e.Enquirer)
            .Include(u => u.Projects)
            .ThenInclude(p => p.ProjectRoles)
            .ThenInclude(pr => pr.Enquiries)
            .ThenInclude(e => e.Messages)
            .Include(u => u.Enquiries)
            .ThenInclude(e => e.Messages)
            .Include(u => u.Enquiries)
            .ThenInclude(e => e.ProjectManager)
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
        var user = await context.Users.Where(u => u.UserId == id).FirstOrDefaultAsync();

        return user;
    }

    public async Task<User?> GetByIdIncludeAllPropertiesAsync(int id)
    {
        var user = await context
            .Users.Where(u => u.UserId == id)
            .Include(u => u.PortofolioContents)
            .ThenInclude(pc => pc.Medias)
            .Include(u => u.Skills)
            .Include(u => u.ReviewsGiven)
            .Include(u => u.ReviewsReceived)
            .Include(u => u.Assets)
            .ThenInclude(a => a.Medias)
            .Include(u => u.Projects)
            .ThenInclude(p => p.Medias)
            .Include(u => u.Projects)
            .ThenInclude(p => p.ProjectRoles)
            .ThenInclude(pr => pr.Medias)
            .Include(u => u.Projects)
            .ThenInclude(p => p.ProjectRoles)
            .ThenInclude(pr => pr.Enquiries)
            .ThenInclude(e => e.Enquirer)
            .Include(u => u.Projects)
            .ThenInclude(p => p.ProjectRoles)
            .ThenInclude(pr => pr.Enquiries)
            .ThenInclude(e => e.Messages)
            .Include(u => u.Enquiries)
            .ThenInclude(e => e.Messages)
            .Include(u => u.Enquiries)
            .ThenInclude(e => e.ProjectManager)
            .FirstOrDefaultAsync();

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

    public async Task<User?> GetByIdIncludePortofolioAsync(int id)
    {
        var user = await context
            .Users.Where(u => u.UserId == id)
            .Include(u => u.PortofolioContents)
            .ThenInclude(pc => pc.Medias)
            .FirstOrDefaultAsync();

        return user;
    }
}
