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
            Enquirer = enquiry.Enquirer?.ToInformationDTO(),
            ProjectManager = enquiry.ProjectManager?.ToInformationDTO(),
            Messages = enquiry.Messages.Select(m => m.ToDTO()).ToList()
        };
    }
}