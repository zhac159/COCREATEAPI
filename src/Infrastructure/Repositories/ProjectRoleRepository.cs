using Domain.Entities;
using Domain.Interfaces;
using Domain.Queries;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class ProjectRoleRepostiory : IProjectRoleRepository
{
    private readonly CoCreateDbContext context;

    public ProjectRoleRepostiory(CoCreateDbContext context)
    {
        this.context = context;
    }

    public async Task<ProjectRole> CreateAsync(ProjectRole projectRole)
    {
        await context.ProjectRoles.AddAsync(projectRole);
        await context.SaveChangesAsync();

        return projectRole;
    }

    public async Task<ProjectRole?> GetByIdAsync(int id)
    {
        return await context.ProjectRoles.FindAsync(id);
    }

    public async Task<ProjectRole?> GetByIdIncludeAllProjectAsync(int id)
    {
        return await context
            .ProjectRoles.Include(pr => pr.Medias)
            .Include(pr => pr.Project)
            .FirstOrDefaultAsync(pr => pr.Id == id);
    }

    public async Task<bool> DeleteAsync(ProjectRole projectRole)
    {
        context.ProjectRoles.Remove(projectRole);
        await context.SaveChangesAsync();

        return true;
    }

    public async Task<ProjectRole> UpdateAsync(ProjectRole projectRole)
    {
        context.ProjectRoles.Update(projectRole);
        await context.SaveChangesAsync();

        return projectRole;
    }

    public async Task<List<(int, Project)>> GetMatchingProjectRoleIdsAsync(
        GetMatchingProjectRolesAsyncQuery query
    )
    {
        var seenProjectRoleIds = await context
            .SeenMatches.Where(sm => sm.UserId == query.UserId)
            .Select(sm => sm.ProjectRoleId)
            .ToListAsync();

        var allProjectRoles = await context
            .ProjectRoles.Include(pr => pr.Project!)
            .ThenInclude(p => p.ProjectRoles)
            .ThenInclude(pr => pr.Assignee)
            .Include(pr => pr.Project!)
            .ThenInclude(p => p.ProjectManager)
            .Include(pr => pr.Project!)
            .ThenInclude(p => p.Medias)
            .Include(pr => pr.Medias)
            .Where(pr => pr.AssigneeId == null)
            .Where(pr => pr.Effort <= query.Effort && query.SkillTypes.Contains(pr.SkillType))
            .Where(pr => !seenProjectRoleIds.Contains(pr.Id))
            .Where(pr => pr.Location.IsWithinDistance(query.Location, query.Distance / 111.12))
            .OrderBy(pr => pr.Project!.ProjectManagerId != query.UserId)
            .Take(1000)
            .ToListAsync();

        var matchingProjectRoles = new List<(int, Project)>();

        foreach (var projectRole in allProjectRoles)
        {
            matchingProjectRoles.Add((projectRole.Id, projectRole.Project!));
        }

        var seenMatches = new List<SeenMatches>();

        // if (matchingProjectRoles.Count < 5)
        // {
        //     var userMatches = context.SeenMatches.Where(sm => sm.UserId == query.UserId);

        //     context.SeenMatches.RemoveRange(userMatches);
        // }
        // else
        // {
        //     context.SeenMatches.AddRange(seenMatches);
        // }

        await context.SaveChangesAsync();

        return matchingProjectRoles;
    }
}
