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
            .Users.AsSplitQuery()
            .Where(u => u.Username == name)
            .Include(u => u.PortofolioContents)
            .ThenInclude(pc => pc.Medias)
            .Include(u => u.Skills)
            .Include(u => u.ReviewsGiven)
            .Include(u => u.ReviewsReceived)
            .Include(u => u.Assets)
            .ThenInclude(a => a.Medias)
            .Include(u => u.Assets)
            .ThenInclude(a => a.AssetOffers)
            .ThenInclude(ao => ao.Project)
            .ThenInclude(p => p!.ProjectManager)
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
            .ThenInclude(p => p.AssetOffers)
            .ThenInclude(ao => ao.Asset)
            .ThenInclude(ao => ao!.User)
            .Include(u => u.Projects)
            .ThenInclude(p => p.ProjectRoles)
            .ThenInclude(pr => pr.Enquiries)
            .Include(u => u.Projects)
            .ThenInclude(p => p.ProjectRoles)
            .ThenInclude(pr => pr.Assignee)
            .Include(u => u.Enquiries)
            .Include(u => u.Enquiries)
            .ThenInclude(e => e.ProjectManager)
            .Include(u => u.ProjectRoles)
            .ThenInclude(pr => pr.Project!)
            .ThenInclude(p => p.ProjectRoles)
            .ThenInclude(pr => pr.Assignee)
            .Include(u => u.ProjectRoles)
            .ThenInclude(pr => pr.Project!)
            .ThenInclude(p => p.Medias)
            .Include(u => u.ProjectRoles)
            .ThenInclude(pr => pr.Project!)
            .ThenInclude(p => p.ProjectManager)
            .Include(u => u.Experiences)
            .ThenInclude(e => e.Medias)
            .Include(u => u.ReviewsReceived)
            .ThenInclude(r => r.ReviewerUser)
            .FirstOrDefaultAsync();

        FilterCompletedProjects(user);

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
            .Users.AsSplitQuery()
            .Where(u => u.UserId == id)
           .Include(u => u.PortofolioContents)
            .ThenInclude(pc => pc.Medias)
            .Include(u => u.Skills)
            .Include(u => u.ReviewsGiven)
            .Include(u => u.ReviewsReceived)
            .Include(u => u.Assets)
            .ThenInclude(a => a.Medias)
            .Include(u => u.Assets)
            .ThenInclude(a => a.AssetOffers)
            .ThenInclude(ao => ao.Project)
            .ThenInclude(p => p!.ProjectManager)
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
            .ThenInclude(p => p.AssetOffers)
            .ThenInclude(ao => ao.Asset)
            .ThenInclude(ao => ao!.User)
            .Include(u => u.Projects)
            .ThenInclude(p => p.ProjectRoles)
            .ThenInclude(pr => pr.Enquiries)
            .Include(u => u.Projects)
            .ThenInclude(p => p.ProjectRoles)
            .ThenInclude(pr => pr.Assignee)
            .Include(u => u.Enquiries)
            .Include(u => u.Enquiries)
            .ThenInclude(e => e.ProjectManager)
            .Include(u => u.ProjectRoles)
            .ThenInclude(pr => pr.Project!)
            .ThenInclude(p => p.ProjectRoles)
            .ThenInclude(pr => pr.Assignee)
            .Include(u => u.ProjectRoles)
            .ThenInclude(pr => pr.Project!)
            .ThenInclude(p => p.Medias)
            .Include(u => u.ProjectRoles)
            .ThenInclude(pr => pr.Project!)
            .ThenInclude(p => p.ProjectManager)
            .Include(u => u.Experiences)
            .ThenInclude(e => e.Medias)
            .Include(u => u.ReviewsReceived)
            .ThenInclude(r => r.ReviewerUser)
            .FirstOrDefaultAsync();

        FilterCompletedProjects(user);

        return user;
    }

    public async Task<List<User>> GetUsersProfileAsync(List<int> ids)
    {
        var users = await context
            .Users.AsSplitQuery()
            .Where(u => ids.Contains(u.UserId))
            .Include(u => u.PortofolioContents)
            .ThenInclude(pc => pc.Medias)
            .Include(u => u.Skills)
            .Include(u => u.ReviewsReceived)
            .ThenInclude(r => r.ReviewerUser)
            .Include(u => u.Experiences)
            .ThenInclude(e => e.Medias)
            .Include(u => u.Experiences)
            .ThenInclude(e => e.Project)
            .Include(u => u.Experiences)
            .ThenInclude(e => e.ProjectRole)
            .ToListAsync();

        return users;
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

    private static void FilterCompletedProjects(User? user)
    {
        if (user is null)
        {
            return;
        }

        user.ProjectRoles = user.ProjectRoles.Where(pr => !pr.Completed).ToList();

        user.Projects = user.Projects.Where(p => !p.Completed).ToList();
    }

    public async Task<bool> UpdateRangeAsync(List<User> users)
    {
        context.Users.UpdateRange(users);
        await context.SaveChangesAsync();

        return true;
    }

    public async Task<List<User>> GetRangeAsync(List<int> ids)
    {
        var users = await context.Users.Where(u => ids.Contains(u.UserId)).ToListAsync();

        return users;
    }
}
