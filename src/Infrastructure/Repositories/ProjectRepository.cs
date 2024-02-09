using Application.Interfaces;
using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class ProjectRepository : IProjectRepository
{
    private readonly CoCreateDbContext context;
    private readonly IStorageService storageService;

    public ProjectRepository(CoCreateDbContext context, IStorageService storageService)
    {
        this.context = context;
        this.storageService = storageService;
    }

    public async Task<Project> CreateAsync(Project project)
    {
        await context.Projects.AddAsync(project);
        await context.SaveChangesAsync();

        project.Uris = project
            .FileSrcs.Select(fileSrc => storageService.GetFileUri(fileSrc, "projects"))
            .ToList();

        return project;
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
            .Include(p => p.ProjectManager)
            .Include(p => p.ProjectRoles)
            .ThenInclude(pr => pr.Assignee)
            .FirstOrDefaultAsync();

        PopulateUris(project);
        
        return project;
    }

    private void PopulateUris(Project? project)
    {
        if (project?.FileSrcs != null)
        {
            project.Uris = project
                .FileSrcs.Select(fileSrc => storageService.GetFileUri(fileSrc, "projects"))
                .ToList();
        }

        if (project?.ProjectRoles != null)
        {
            foreach (var projectRole in project.ProjectRoles)
            {
                projectRole.Uris = projectRole
                    .FileSrcs.Select(fileSrc => storageService.GetFileUri(fileSrc, "projectroles"))
                    .ToList();
            }
        }
    }
}
