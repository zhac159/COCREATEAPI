using Application.DTOs.ExperienceDTOs;
using Application.Extensions;
using Application.Interfaces;
using Domain.Enums;
using Domain.Exceptions;
using Domain.Interfaces;

namespace Application.Services;

public class ExperienceService : IExperienceService
{
    private readonly IExperienceRepository experienceRepository;
    private readonly IProjectRepository projectRepository;
    private readonly IProjectRoleRepository projectRoleRepository;
    private readonly ICurrentUserContextService currentUserContextService;

    public ExperienceService(
        IExperienceRepository experienceRepository,
        IProjectRepository projectRepository,
        IProjectRoleRepository projectRoleRepository,
        ICurrentUserContextService currentUserContextService
    )
    {
        this.experienceRepository = experienceRepository;
        this.projectRepository = projectRepository;
        this.projectRoleRepository = projectRoleRepository;
        this.currentUserContextService = currentUserContextService;
    }

    public async Task<ExperienceDTO> CreateAsync(ExperienceCreateDTO experienceCreateDTO)
    {
        var userId = currentUserContextService.GetUserId();

        var experience = experienceCreateDTO.ToEntity(userId);

        if (
            experience.ExperienceType.Equals(ExperienceType.ProjectRole)
            && experience.ProjectRoleId != null
        )
        {
            var projectRole = await projectRoleRepository.GetByIdIncludeAllPropertiesAsync(
                experience.ProjectRoleId.Value
            );

            if (projectRole is null)
            {
                throw new EntityNotFoundException();
            }

            if (projectRole.AssigneeId != userId)
            {
                throw new UnauthorizedAccessException();
            }

            if (!projectRole.Project!.Completed)
            {
                throw new ProjectNotCompletedExeption();
            }

            experience.ProjectId = projectRole.ProjectId;

        }

        var createdExperience = await experienceRepository.CreateAsync(experience);

        return createdExperience.ToDTO();
    }
}
