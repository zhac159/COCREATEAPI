using Application.DTOs.ProjectRoleDTOs;
using Application.Extensions;
using Application.Interfaces;
using Domain.Exceptions;
using Domain.Interfaces;

namespace Application.Services;

public class ProjectRoleService : IProjectRoleService
{
    private readonly IProjectRoleRepository projectRoleRepository;
    private readonly IProjectRepository projectRepository;
    private readonly IUserRepository userRepository;
    private readonly ICurrentUserContextService currentUserContextService;
    private readonly IStorageService storageService;

    public ProjectRoleService(
        IProjectRoleRepository projectRoleRepository,
        IProjectRepository projectRepository,
        IUserRepository userRepository,
        ICurrentUserContextService currentUserContextService,
        IStorageService storageService
    )
    {
        this.projectRoleRepository = projectRoleRepository;
        this.projectRepository = projectRepository;
        this.userRepository = userRepository;
        this.currentUserContextService = currentUserContextService;
        this.storageService = storageService;
    }

    public async Task<ProjectRoleDTO> CreateAsync(
        ProjectRoleCreateDTO projectRoleCreateDTO
    )
    {
        var coins = await userRepository.GetCoinByIdAsync(currentUserContextService.GetUserId());

        if (coins < projectRoleCreateDTO.Cost || coins is null)
        {
            throw new InsufficientFundsException();
        }

        var project = await projectRepository.GetByIdAsync(
            projectRoleCreateDTO.ProjectId
        );

        if (project is null)
        {
            throw new EntityNotFoundException();
        }

        if (project.ProjectManagerId != currentUserContextService.GetUserId())
        {
            throw new UnauthorizedAccessException();
        }

        var projectRole = projectRoleCreateDTO.ToEntity();

        var createdProjectRole = await projectRoleRepository.CreateAsync(projectRole);

        await userRepository.UpdateCoinByIdAsync(
            currentUserContextService.GetUserId(),
            coins.Value - projectRoleCreateDTO.Cost
        );

        return createdProjectRole.ToDTO();
    }

    public async Task<ProjectRoleDTO> UpdateAsync(
        ProjectRoleUpdateDTO projectRoleUpdateDTO
    )
    {
        var projectRole = await projectRoleRepository.GetByIdAsync(
            projectRoleUpdateDTO.Id
        );

        if (projectRole is null)
        {
            throw new EntityNotFoundException();
        }

        var project = await projectRepository.GetByIdAsync(
            projectRole.ProjectId
        );

        if (project is null)
        {
            throw new EntityNotFoundException();
        }

        if (project.ProjectManagerId != currentUserContextService.GetUserId())
        {
            throw new UnauthorizedAccessException();
        }

        await projectRole.UpdateFromDTOAsync(projectRoleUpdateDTO, storageService);

        await projectRoleRepository.UpdateAsync(projectRole);

        return projectRole.ToDTO();
    }
}
