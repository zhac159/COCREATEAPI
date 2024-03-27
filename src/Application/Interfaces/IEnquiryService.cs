using Application.DTOs.EnquiryDTOs;

namespace Application.Interfaces;

public interface IEnquiryService
{
    Task<EnquiryDTO> CreateAsync(EnquiryCreateDTO enquiryDTO);
    Task<bool> ConfirmAsync(EnquiryConfirmDTO enquiryConfirmDTO);
}