using Application.DTOs.ProjectDTOs;
using Application.Extensions;
using Application.Interfaces;
using Domain.Enums;
using Domain.Exceptions;
using Domain.Interfaces;

namespace Application.Services;

public class ProjectService : IProjectService
{
    private readonly IProjectRepository projectRepository;
    private readonly ICurrentUserContextService currentUserContextService;
    
    private readonly IMessageStorageService messageStorageService;

    public ProjectService(IProjectRepository projectRepository, ICurrentUserContextService currentUserContextService, IMessageStorageService messageStorageService)
    {
        this.projectRepository = projectRepository;
        this.currentUserContextService = currentUserContextService;
        this.messageStorageService = messageStorageService;
    }

    public async Task<ProjectDTO> CreateAsync(
        ProjectCreateDTO projectCreateDTO
    )
    {
        var project = projectCreateDTO.ToEntity(currentUserContextService.GetUserId());

        var createdProject = await projectRepository.CreateAsync(project);

        await messageStorageService.AddMemberToGroupChatAsync(createdProject.Id, ChatType.Project, currentUserContextService.GetUserId());

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
