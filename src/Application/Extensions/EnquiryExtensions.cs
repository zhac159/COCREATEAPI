using Application.DTOs.AssetDTOs;
using Application.DTOs.EnquiryDTOs;
using Domain.Entities;

namespace Application.Extensions;

public static class EnquiryExtensions
{
    public static EnquiryDTO ToDTO(this Enquiry enquiry)
    {
        return new EnquiryDTO
        {
            Id = enquiry.Id,
            ProjectRoleId = enquiry.ProjectRoleId,
            UserId = enquiry.UserId
        };
    }
}