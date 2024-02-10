using Application.DTOs.AssetDTOs;
using Application.DTOs.EnquiryDTOs;
using Domain.Entities;

namespace Application.Interfaces;

public interface IEnquiryService
{
    Task<EnquiryDTO> CreateAsync(EnquiryCreateDTO enquiryDTO, int userId);
    Task<bool> ConfirmAsync(EnquiryConfirmDTO enquiryConfirmDTO, int userId);
}