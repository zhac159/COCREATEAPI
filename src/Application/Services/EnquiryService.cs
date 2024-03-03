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

    public EnquiryService(
        IEnquiryRepository enquiryRepository,
        IProjectRoleRepository projectRoleRepository,
        IUserRepository userRepository,
        IStorageService storageService
    )
    {
        this.enquiryRepository = enquiryRepository;
        this.projectRoleRepository = projectRoleRepository;
        this.userRepository = userRepository;
    }

    public async Task<EnquiryDTO> CreateAsync(EnquiryCreateDTO enquiryDTO, int userId)
    {
        var projectRole = await projectRoleRepository.GetByIdAsync(enquiryDTO.ProjectRoleId);

        if (projectRole is null)
        {
            throw new EntityNotFoundException();
        }

        if (projectRole.AssigneeId != null)
        {
            throw new RoleAlreadyFilledException();
        }

        var user = await userRepository.GetByIdAsync(userId);

        if (user is null)
        {
            throw new EntityNotFoundException();
        }

        var enquiry = enquiryDTO.ToEntity(userId);

        var createdEnquiry = await enquiryRepository.CreateAsync(enquiry);

        return createdEnquiry.ToDTO();
    }

    public async Task<bool> ConfirmAsync(EnquiryConfirmDTO enquiryConfirmDTO, int userId)
    {
        var enquiry = await enquiryRepository.GetByIdAsync(enquiryConfirmDTO.EnquiryId);

        if (enquiry is null)
        {
            throw new EntityNotFoundException();
        }

        var projectRole = await projectRoleRepository.GetByIdIncludeAllProjectAsync(enquiry.ProjectRoleId);

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

        if (projectRole.Project.ProjectManagerId != userId)
        {
            throw new UnauthorizedAccessException();
        }

        projectRole.AssigneeId = enquiry.UserId;

        await projectRoleRepository.UpdateAsync(projectRole);

        return true;
    }
}
