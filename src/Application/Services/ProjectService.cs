using Application.DTOs.ProjectDTOs;
using Application.Extensions;
using Application.Interfaces;
using Domain.Exceptions;
using Domain.Interfaces;

namespace Application.Services;

public class ProjectService : IProjectService
{
    private readonly IProjectRepository projectRepository;
    private readonly ICurrentUserContextService currentUserContextService;
    private readonly IStorageService storageService;

    public ProjectService(IProjectRepository projectRepository, ICurrentUserContextService currentUserContextService, IStorageService storageService)
    {
        this.projectRepository = projectRepository;
        this.currentUserContextService = currentUserContextService;
        this.storageService = storageService;
    }

    public async Task<ProjectDTO> CreateAsync(
        ProjectCreateDTO projectCreateDTO
    )
    {
        var project = projectCreateDTO.ToEntity(currentUserContextService.GetUserId());

        var createdProject = await projectRepository.CreateAsync(project);

        return createdProject.ToDTO();
    }

    public async Task<ProjectDTO?> GetByIdAsync(int id)
    {
        var project = await projectRepository.GetByIdIncludeAllPropertiesAsync(id);

        if (project is null)
        {
            throw new EntityNotFoundException();
        }

        return project.ToDTO();
    }
}
