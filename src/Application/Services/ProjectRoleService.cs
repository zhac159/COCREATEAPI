using System.Runtime.Serialization;
using System.Text;
using Application.DTOs.ProjectDTOs;
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
    private readonly IStorageService storageService;

    public ProjectRoleService(
        IProjectRoleRepository projectRoleRepository,
        IStorageService storageService,
        IProjectRepository projectRepository,
        IUserRepository userRepository
    )
    {
        this.projectRoleRepository = projectRoleRepository;
        this.storageService = storageService;
        this.projectRepository = projectRepository;
        this.userRepository = userRepository;
    }

    public async Task<ProjectRoleDTO> CreateAsync(
        ProjectRoleCreateWrapperDTO projectRoleCreateWrapperDTO,
        int userId
    )
    {
        var coins = await userRepository.GetCoinByIdAsync(userId);

        if (coins < projectRoleCreateWrapperDTO.ProjectRoleCreateDTO.Cost || coins is null)
        {
            throw new InsufficientFundsException();
        }

        var project = await projectRepository.GetByIdAsync(
            projectRoleCreateWrapperDTO.ProjectRoleCreateDTO.ProjectId
        );

        if (project is null)
        {
            throw new EntityNotFoundException();
        }

        if (project.ProjectManagerId != userId)
        {
            throw new UnauthorizedAccessException();
        }

        var fileSrcs = new List<string>();

        if (
            projectRoleCreateWrapperDTO.ProjectRoleCreateDTO.Name == "null"
            || projectRoleCreateWrapperDTO.ProjectRoleCreateDTO.Name == "undefined"
        )
        {
            throw new InvalidModelException();
        }

        for (int i = 0; i < projectRoleCreateWrapperDTO.MediaFiles.Count; i++)
        {
            var fileSrc = new StringBuilder()
                .Append("projectrole_")
                .Append(userId)
                .Append("_")
                .Append(projectRoleCreateWrapperDTO.ProjectRoleCreateDTO.Name)
                .Append("_")
                .Append(Guid.NewGuid().ToString())
                .Append(".jpeg")
                .ToString();

            await storageService.UploadFile(
                fileSrc,
                projectRoleCreateWrapperDTO.MediaFiles[i],
                "projectroles"
            );

            fileSrcs.Add(fileSrc);
        }

        var projectRole = projectRoleCreateWrapperDTO.ProjectRoleCreateDTO.ToEntity(fileSrcs);

        var createdProjectRole = await projectRoleRepository.CreateAsync(projectRole);

        await userRepository.UpdateCoinByIdAsync(
            userId,
            coins.Value - projectRoleCreateWrapperDTO.ProjectRoleCreateDTO.Cost
        );

        return createdProjectRole.ToDTO();
    }
}
