using Application.Interfaces;
using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class ProjectRepository : IProjectRepository
{
    private readonly CoCreateDbContext context;

    public ProjectRepository(CoCreateDbContext context)
    {
        this.context = context;
    }

    public async Task<Project?> CreateAsync(Project project)
    {
        await context.Projects.AddAsync(project);
        await context.SaveChangesAsync();

        var createdProject = await GetByIdIncludeAllPropertiesAsync(project.Id);

        return createdProject;
    }

    public async Task<Project?> GetByIdAsync(int id)
    {
        return await context.Projects.FindAsync(id);
    }

    public async Task<bool> DeleteAsync(Project project)
    {
        context.Projects.Remove(project);
        await context.SaveChangesAsync();

        return true;
    }

    public async Task<Project> UpdateAsync(Project project)
    {
        context.Projects.Update(project);
        await context.SaveChangesAsync();

        return project;
    }

    public async Task<Project?> GetByIdIncludeAllPropertiesAsync(int id)
    {
        var project = await context
            .Projects.Where(p => p.Id == id)
            .Include(p => p.Medias)
            .Include(p => p.ProjectManager)
            .Include(p => p.ProjectRoles)
            .ThenInclude(pr => pr.Medias)
            .Include(p => p.ProjectRoles)
            .ThenInclude(pr => pr.Assignee)
            .FirstOrDefaultAsync();

        return project;
    }

    public async Task<Project?> GetByIdIncludeAllExperiencesAsync(int id)
    {
        var project = await context
            .Projects.Where(p => p.Id == id)
            .Include(p => p.Medias)
            .Include(p => p.Experiences)
            .ThenInclude(e => e.Medias)
            .Include(p => p.ProjectManager)
            .Include(p => p.ProjectRoles)
            .ThenInclude(pr => pr.Medias)
            .Include(p => p.ProjectRoles)
            .ThenInclude(pr => pr.Assignee)
            .Include(p => p.ProjectRoles)
            .ThenInclude(pr => pr.Experiences)
            .ThenInclude(e => e.Medias)
            .FirstOrDefaultAsync();

        return project;
    }

    public async Task<Project?> GetProjectByRoleIdAsync(int roleId)
    {
        var project = await context
            .Projects
            .Include(p => p.Medias)
            .Include(p => p.ProjectManager)
            .Include(p => p.ProjectRoles)
            .ThenInclude(pr => pr.Medias)
            .Include(p => p.ProjectRoles)
            .ThenInclude(pr => pr.Assignee)
            .FirstOrDefaultAsync(p => p.ProjectRoles.Any(pr => pr.Id == roleId));

        return project;
    }
}
