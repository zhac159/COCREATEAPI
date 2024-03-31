using Application.DTOs.EnquiryDTOs;
using Application.Extensions;
using Application.Interfaces;
using Domain.Exceptions;
using Domain.Interfaces;

namespace Application.Services;

public class EnquiryService : IEnquiryService
{
    private readonly IEnquiryRepository enquiryRepository;
    private readonly IProjectRoleRepository projectRoleRepository;
    private readonly IUserRepository userRepository;
    private readonly ICurrentUserContextService currentUserContextService;

    public EnquiryService(
        IEnquiryRepository enquiryRepository,
        IProjectRoleRepository projectRoleRepository,
        IUserRepository userRepository,
        ICurrentUserContextService currentUserContextService
    )
    {
        this.enquiryRepository = enquiryRepository;
        this.projectRoleRepository = projectRoleRepository;
        this.userRepository = userRepository;
        this.currentUserContextService = currentUserContextService;
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

        // await messageStorageService.AddChatAsync(
        //     createdEnquiryDTO.Id,
        //     ChatType.Private,
        //     new List<int> {createdEnquiry.EnquirerId, createdEnquiry.ProjectManagerId}
        // );

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

        return true;
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
}
