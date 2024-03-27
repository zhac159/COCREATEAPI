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
    private readonly IChatHubService chatHubService;
    private readonly ICurrentUserContextService currentUserContextService;
    private readonly IMessageStorageService messageStorageService;

    public EnquiryService(
        IEnquiryRepository enquiryRepository,
        IProjectRoleRepository projectRoleRepository,
        IUserRepository userRepository,
        IChatHubService chatHubService,
        ICurrentUserContextService currentUserContextService,
        IMessageStorageService messageStorageService
    )
    {
        this.enquiryRepository = enquiryRepository;
        this.projectRoleRepository = projectRoleRepository;
        this.userRepository = userRepository;
        this.chatHubService = chatHubService;
        this.currentUserContextService = currentUserContextService;
        this.messageStorageService = messageStorageService;
    }

    public async Task<EnquiryDTO> CreateAsync(EnquiryCreateDTO enquiryDTO)
    {
        var projectRole = await projectRoleRepository.GetByIdIncludeAllProjectAsync(
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

        var createdEnquiry = await enquiryRepository.CreateAsync(enquiry);

        var createdEnquiryDTO = createdEnquiry.ToDTO();

        await messageStorageService.AddChatAsync(
            createdEnquiryDTO.Id,
            ChatType.Enquiry,
            new List<int> {createdEnquiry.EnquirerId, createdEnquiry.ProjectManagerId}
        );

        return createdEnquiryDTO;
    }

    public async Task<bool> ConfirmAsync(EnquiryConfirmDTO enquiryConfirmDTO)
    {
        var enquiry = await enquiryRepository.GetByIdAsync(enquiryConfirmDTO.EnquiryId);

        if (enquiry is null)
        {
            throw new EntityNotFoundException();
        }

        var projectRole = await projectRoleRepository.GetByIdIncludeAllProjectAsync(
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

        return true;
    }

    // public async Task<EnquiryMessageCreateDTO> SendMessageAsync(
    //     EnquiryMessageCreateDTO enquiryMessageCreateDTO
    // )
    // {
    //     var enquiry = await enquiryRepository.GetByIdAsync(enquiryMessageCreateDTO.EnquiryId);

    //     if (enquiry is null)
    //     {
    //         throw new EntityNotFoundException();
    //     }

    //     var user = await userRepository.GetByIdAsync(currentUserContextService.GetUserId());

    //     if (user is null)
    //     {
    //         throw new EntityNotFoundException();
    //     }

    //     if (user.UserId != enquiry.EnquirerId && user.UserId != enquiry.ProjectManagerId)
    //     {
    //         throw new UnauthorizedAccessException();
    //     }

    //     var message = enquiryMessageCreateDTO.ToEntity(user.UserId);

    //     enquiry.Messages.Add(message);

    //     await enquiryRepository.UpdateAsync(enquiry);

    //     var receiverId =
    //         enquiry.EnquirerId == user.UserId ? enquiry.ProjectManagerId : enquiry.EnquirerId;
            
    //     await chatHubService.SendEnquiryMessageAsync(message.ToDTO(), receiverId);

    //     return enquiryMessageCreateDTO;
    // }
}
