using Application.Interfaces;
using Domain.Entities;
using Domain.Interfaces;
using Domain.Queries;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class ProjectRoleRepostiory : IProjectRoleRepository
{
    private readonly CoCreateDbContext context;
    private readonly IStorageService storageService;

    public ProjectRoleRepostiory(CoCreateDbContext context, IStorageService storageService)
    {
        this.context = context;
        this.storageService = storageService;
    }

    public async Task<ProjectRole> CreateAsync(ProjectRole projectRole)
    {
        await context.ProjectRoles.AddAsync(projectRole);
        await context.SaveChangesAsync();

        projectRole.Uris = projectRole
            .FileSrcs.Select(fileSrc => storageService.GetFileUri(fileSrc, "projectroles"))
            .ToList();

        return projectRole;
    }

    public async Task<ProjectRole?> GetByIdAsync(int id)
    {
        return await context.ProjectRoles.FindAsync(id);
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
            .Where(pr => pr.Effort <= query.Effort && query.SkillTypes.Contains(pr.SkillType))
            .Where(pr => !seenProjectRoleIds.Contains(pr.Id))
            .Take(5)
            .ToListAsync();

        var matchingProjectRoles = new List<(int, Project)>();

        foreach (var projectRole in allProjectRoles)
        {
            var distance = CalculateDistance(
                query.Latitude,
                query.Longitude,
                projectRole.Latitude,
                projectRole.Longitude
            );

            if (distance <= query.Distance && projectRole.Project != null)
            {
                matchingProjectRoles.Add((projectRole.Id, projectRole.Project));
                
                context.SeenMatches.Add(
                    new SeenMatches
                    {
                        UserId = query.UserId,
                        ProjectRoleId = projectRole.Id,
                        SeenAt = DateTime.Now
                    }
                );
            }
        }

        await context.SaveChangesAsync();

        return matchingProjectRoles;
    }

    private double CalculateDistance(double lat1, double lon1, double lat2, double lon2)
    {
        var d1 = lat1 * (Math.PI / 180.0);
        var num1 = lon1 * (Math.PI / 180.0);
        var d2 = lat2 * (Math.PI / 180.0);
        var num2 = lon2 * (Math.PI / 180.0) - num1;
        var d3 =
            Math.Pow(Math.Sin((d2 - d1) / 2.0), 2.0)
            + Math.Cos(d1) * Math.Cos(d2) * Math.Pow(Math.Sin(num2 / 2.0), 2.0);

        return 6376500.0 * (2.0 * Math.Atan2(Math.Sqrt(d3), Math.Sqrt(1.0 - d3)));
    }
}
