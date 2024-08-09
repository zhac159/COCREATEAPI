using Application.DTOs.ProjectRoleDTOs;
using Application.Extensions;
using Application.Interfaces;
using Domain.Entities;
using Domain.Exceptions;
using Domain.Interfaces;

namespace Application.Services;

public class ProjectRoleService : IProjectRoleService
{
    private readonly IProjectRoleRepository projectRoleRepository;
    private readonly IProjectRepository projectRepository;
    private readonly IUserService userService;
    private readonly ICurrentUserContextService currentUserContextService;
    private readonly IStorageService storageService;
    private readonly IExperienceRepository experienceRepository;
    private readonly IReviewService reviewService;

    public ProjectRoleService(
        IProjectRoleRepository projectRoleRepository,
        IProjectRepository projectRepository,
        ICurrentUserContextService currentUserContextService,
        IStorageService storageService,
        IExperienceRepository experienceRepository,
        IReviewService reviewService,
        IUserService userService
    )
    {
        this.projectRoleRepository = projectRoleRepository;
        this.projectRepository = projectRepository;
        this.currentUserContextService = currentUserContextService;
        this.storageService = storageService;
        this.experienceRepository = experienceRepository;
        this.reviewService = reviewService;
        this.userService = userService;
    }

    public async Task<ProjectRoleDTO> CreateAsync(ProjectRoleCreateDTO projectRoleCreateDTO)
    {
        var project = await projectRepository.GetByIdAsync(projectRoleCreateDTO.ProjectId);

        if (project is null)
        {
            throw new EntityNotFoundException();
        }

        if (project.ProjectManagerId != currentUserContextService.GetUserId())
        {
            throw new UnauthorizedAccessException();
        }

        var projectRole = projectRoleCreateDTO.ToEntity();

        await userService.AdjustUserCoinsAsync(-projectRole.Cost);

        var createdProjectRole = await projectRoleRepository.CreateAsync(projectRole);

        return createdProjectRole.ToDTO();
    }

    public async Task<ProjectRoleDTO> UpdateAsync(ProjectRoleUpdateDTO projectRoleUpdateDTO)
    {
        var projectRole = await projectRoleRepository.GetByIdAsync(projectRoleUpdateDTO.Id);

        if (projectRole is null)
        {
            throw new EntityNotFoundException();
        }

        var project = await projectRepository.GetByIdAsync(projectRole.ProjectId);

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

    public async Task<bool> CompleteAsync(ProjectRoleCompleteDTO projectRoleCompleteDTO)
    {
        var userId = currentUserContextService.GetUserId();

        var projectRole = await projectRoleRepository.GetByIdIncludeAllPropertiesAsync(
            projectRoleCompleteDTO.Id
        );

        if (projectRole is null)
        {
            throw new EntityNotFoundException();
        }

        if (projectRole.AssigneeId != userId)
        {
            throw new UnauthorizedAccessException();
        }

        if (projectRole.Completed)
        {
            throw new ProjectRoleAlreadyCompletedException();
        }

        if (!projectRole.Project!.Completed)
        {
            throw new ProjectNotCompletedExeption();
        }

        projectRole.Completed = true;

        var experience = projectRoleCompleteDTO.ToExperienceEntity(userId);

        var reviews = projectRoleCompleteDTO
            .Reviews.Select(review => review.ToEntity(userId))
            .ToList();

        ValidateAndCleanReviews(projectRole, reviews);

        await reviewService.CreateRangeFromEntitiesAsync(reviews);
        await experienceRepository.CreateAsync(experience);
        await projectRoleRepository.UpdateAsync(projectRole);
        await userService.AdjustUserCoinsAsync(projectRole.Cost);

        return true;
    }

    public async Task<ProjectRoleDTO> GetAsync(int id)
    {
        var projectRole =
            await projectRoleRepository.GetByIdIncludeAllPropertiesAsync(id)
            ?? throw new EntityNotFoundException();
       
        return projectRole.ToDTO();
    }

    private static void ValidateAndCleanReviews(ProjectRole projectRole, List<Review> reviews)
    {
        if (!reviews.Any(review => review.ReviewedUserId == projectRole.Project!.ProjectManagerId))
        {
            throw new MissingReviewException();
        }
    }
}
