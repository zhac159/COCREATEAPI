using Application.DTOs.EnquiryDTOs;
using Application.DTOs.ProjectDTOs;
using Application.Extensions;
using Application.Interfaces;
using Domain.Enums;
using Domain.Exceptions;
using Domain.Interfaces;

namespace Application.Services;

public class EnquiryService : IEnquiryService
{
    private readonly IEnquiryRepository enquiryRepository;
    private readonly IProjectRoleRepository projectRoleRepository;
    private readonly IUserRepository userRepository;
    private readonly ICurrentUserContextService currentUserContextService;
    private readonly IMessageStorageService messageStorageService;
    private readonly IChatHubService chatHubService;
    private readonly IProjectRepository projectRepository;

    public EnquiryService(
        IEnquiryRepository enquiryRepository,
        IProjectRoleRepository projectRoleRepository,
        IUserRepository userRepository,
        ICurrentUserContextService currentUserContextService,
        IMessageStorageService messageStorageService,
        IChatHubService chatHubService,
        IProjectRepository projectRepository
    )
    {
        this.enquiryRepository = enquiryRepository;
        this.projectRoleRepository = projectRoleRepository;
        this.userRepository = userRepository;
        this.currentUserContextService = currentUserContextService;
        this.messageStorageService = messageStorageService;
        this.chatHubService = chatHubService;
        this.projectRepository = projectRepository;
    }

    public async Task<EnquiryDTO> CreateAsync(EnquiryCreateDTO enquiryDTO)
    {
        var projectRole = await projectRoleRepository.GetByIdIncludeAllPropertiesAsync(
            enquiryDTO.ProjectRoleId
        );

        if (projectRole is null)
        {
            throw new EntityNotFoundException();
        }

        if (projectRole.AssigneeId != null)
        {
            throw new RoleAlreadyFilledException();
        }

        var user = await userRepository.GetByIdAsync(currentUserContextService.GetUserId());

        if (user is null)
        {
            throw new EntityNotFoundException();
        }

        var enquiry = enquiryDTO.ToEntity(user.UserId, projectRole.Project!.ProjectManagerId);

        await enquiryRepository.CreateAsync(enquiry);

        var createdEnquiry = await enquiryRepository.GetByIdIncludeAllAsync(enquiry.Id);

        if (createdEnquiry is null)
        {
            throw new EntityNotFoundException();
        }

        var createdEnquiryDTO = createdEnquiry.ToDTO();

        await chatHubService.SendNewEnquiry(createdEnquiryDTO);

        return createdEnquiryDTO;
    }

    public async Task<bool> ShortlistAsync(int enquiryId)
    {
        var enquiry = await enquiryRepository.GetByIdAsync(enquiryId);

        if (enquiry is null)
        {
            throw new EntityNotFoundException();
        }

        if (enquiry.ProjectManagerId != currentUserContextService.GetUserId())
        {
            throw new UnauthorizedAccessException();
        }

        enquiry.Shortlisted = true;

        await enquiryRepository.UpdateAsync(enquiry);

        await chatHubService.SendNewShortlist(enquiry);

        return true;
    }

    public async Task<ProjectDTO> ConfirmAsync(EnquiryConfirmDTO enquiryConfirmDTO)
    {
        var enquiry = await enquiryRepository.GetByIdAsync(enquiryConfirmDTO.EnquiryId) ?? throw new EntityNotFoundException();
        
        var projectRole = await projectRoleRepository.GetByIdIncludeAllPropertiesAsync(
            enquiry.ProjectRoleId
        );

        if (projectRole is null)
        {
            throw new EntityNotFoundException();
        }

        if (projectRole.AssigneeId != null)
        {
            throw new RoleAlreadyFilledException();
        }

        if (projectRole.Project == null)
        {
            throw new EntityNotFoundException();
        }

        if (projectRole.Project.ProjectManagerId != currentUserContextService.GetUserId())
        {
            throw new UnauthorizedAccessException();
        }

        projectRole.AssigneeId = enquiry.EnquirerId;

        await projectRoleRepository.UpdateAsync(projectRole);

        await enquiryRepository.DeleteAsync(enquiry);

        await messageStorageService.AddMemberToGroupChatAsync(
            messageStorageService.GetChatId(ChatType.Project, projectRole.ProjectId),
            enquiry.EnquirerId
        );

        var project = await projectRepository.GetByIdIncludeAllPropertiesAsync(projectRole.ProjectId) ?? throw new EntityNotFoundException();

        return project.ToDTO();
    }

    public async Task<bool> RejectAsync(EnquiryRejectDTO enquiryRejectDTO)
    {
        var enquiry = await enquiryRepository.GetByIdAsync(enquiryRejectDTO.EnquiryId);

        var userId = currentUserContextService.GetUserId();

        if (enquiry is null)
        {
            throw new EntityNotFoundException();
        }

        if (userId != enquiry.ProjectManagerId && userId != enquiry.EnquirerId)
        {
            throw new UnauthorizedAccessException();
        }

        await enquiryRepository.DeleteAsync(enquiry);

        return true;
    }
}
