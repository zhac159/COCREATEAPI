using Application.DTOs.ProjectDTOs;
using Application.Extensions;
using Application.Interfaces;
using Domain.Entities;
using Domain.Enums;
using Domain.Exceptions;
using Domain.Interfaces;

namespace Application.Services;

public class ProjectService : IProjectService
{
    private readonly IProjectRepository projectRepository;
    private readonly ICurrentUserContextService currentUserContextService;
    private readonly IExperienceRepository experienceRepository;
    private readonly IMessageStorageService messageStorageService;
    private readonly IReviewService reviewService;
    private readonly IChatHubService chatHubService;
    private readonly IUserRepository userRepository;

    public ProjectService(
        IProjectRepository projectRepository,
        ICurrentUserContextService currentUserContextService,
        IMessageStorageService messageStorageService,
        IExperienceRepository experienceRepository,
        IReviewService reviewService,
        IChatHubService chatHubService,
        IUserRepository userRepository
    )
    {
        this.projectRepository = projectRepository;
        this.currentUserContextService = currentUserContextService;
        this.messageStorageService = messageStorageService;
        this.reviewService = reviewService;
        this.experienceRepository = experienceRepository;
        this.chatHubService = chatHubService;
        this.userRepository = userRepository;
    }

    public async Task<ProjectDTO> CreateAsync(ProjectCreateDTO projectCreateDTO)
    {
        var project = projectCreateDTO.ToEntity(currentUserContextService.GetUserId());

        var createdProject = await projectRepository.CreateAsync(project);

        if (createdProject is null)
        {
            throw new EntityNotFoundException();
        }

        await messageStorageService.AddMemberToGroupChatAsync(
            messageStorageService.GetChatId(ChatType.Project, createdProject.Id),
            currentUserContextService.GetUserId()
        );

        var createdProjectDTO = createdProject.ToDTO();

        return createdProjectDTO;
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

    public async Task<bool> CompleteAsync(ProjectCompleteDTO projectCompleteDTO)
    {
        var project = await projectRepository.GetByIdIncludeAllPropertiesAsync(
            projectCompleteDTO.Id
        );

        if (project is null)
        {
            throw new EntityNotFoundException();
        }

        if (project.ProjectManagerId != currentUserContextService.GetUserId())
        {
            throw new UnauthorizedAccessException();
        }

        if (project.Completed)
        {
            throw new ProjectAlreadyCompletedException();
        }

        project.Completed = true;

        project.CompletedAt = DateTime.UtcNow;

        var experience = projectCompleteDTO.ToExperienceEntity(project.ProjectManagerId);

        var reviews = projectCompleteDTO
            .Reviews.Select(review => review.ToEntity(project.ProjectManagerId))
            .ToList();

        var coinsRefunded = project
            .ProjectRoles.Where(pr => pr.AssigneeId != null)
            .Sum(pr => pr.Cost);

        ValidateAndCleanReviews(project, reviews);

        await reviewService.CreateRangeFromEntitiesAsync(reviews);

        await experienceRepository.CreateAsync(experience);

        await projectRepository.UpdateAsync(project);

        await userRepository.AddCoinsByIdAsync(project.ProjectManagerId, coinsRefunded);

        await chatHubService.SendCompleteProject(project);

        return true;
    }

    public async Task<ProjectCompletedDTO> GetCompletedProjectByIdAsync(int id)
    {
        var project = await projectRepository.GetByIdIncludeAllExperiencesAsync(id);

        if (project is null)
        {
            throw new EntityNotFoundException();
        }

        if (!project.Completed)
        {
            throw new ProjectNotCompletedException();
        }

        return project.ToCompletedDTO();
    }

    public async Task<ProjectDTO?> GetProjectByRoleIdAsync(int roleId)
    {
        var project = await projectRepository.GetProjectByRoleIdAsync(roleId);

        if (project is null)
        {
            throw new EntityNotFoundException();
        }

        return project.ToDTO();
    }

    private static void ValidateAndCleanReviews(Project project, List<Review> reviews)
    {
        var assigneeIds = project
            .ProjectRoles.Select(pr => pr.AssigneeId)
            .Where(id => id != null)
            .ToList();

        reviews.RemoveAll(r => !assigneeIds.Contains(r.ReviewedUserId));

        var firstReviews = reviews.GroupBy(r => r.ReviewedUserId).Select(g => g.First()).ToList();

        foreach (var assigneeId in assigneeIds)
        {
            if (!firstReviews.Any(r => r.ReviewedUserId == assigneeId))
            {
                throw new MissingReviewException();
            }
        }
    }
}
