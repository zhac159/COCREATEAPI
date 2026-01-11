using Application.DTOs.EnquiryDTOs;
using Application.DTOs.ProjectDTOs;

namespace Application.Interfaces;

public interface IEnquiryService
{
    Task<EnquiryDTO> CreateAsync(EnquiryCreateDTO enquiryDTO);
    Task<ProjectDTO> ConfirmAsync(EnquiryConfirmDTO enquiryConfirmDTO);
    Task<bool> RejectAsync(EnquiryRejectDTO enquiryRejectDTO);
    Task<bool> ShortlistAsync(int enquiryId);
}