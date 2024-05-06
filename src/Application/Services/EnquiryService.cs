using Application.DTOs.EnquiryDTOs;
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

    public EnquiryService(
        IEnquiryRepository enquiryRepository,
        IProjectRoleRepository projectRoleRepository,
        IUserRepository userRepository,
        ICurrentUserContextService currentUserContextService,
        IMessageStorageService messageStorageService,
        IChatHubService chatHubService
    )
    {
        this.enquiryRepository = enquiryRepository;
        this.projectRoleRepository = projectRoleRepository;
        this.userRepository = userRepository;
        this.currentUserContextService = currentUserContextService;
        this.messageStorageService = messageStorageService;
        this.chatHubService = chatHubService;
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

    public async Task<bool> ConfirmAsync(EnquiryConfirmDTO enquiryConfirmDTO)
    {
        var enquiry = await enquiryRepository.GetByIdAsync(enquiryConfirmDTO.EnquiryId);

        if (enquiry is null)
        {
            throw new EntityNotFoundException();
        }

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
            projectRole.Project.Id,
            ChatType.Project,
            enquiry.EnquirerId
        );

        return true;
    }
}
