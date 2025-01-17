using Application.DTOs.Chat;
using Application.DTOs.EnquiryDTOs;
using Domain.Entities;
using Domain.Enums;

namespace Application.Extensions;

public static class EnquiryExtensions
{
    public static EnquiryDTO ToDTO(this Enquiry enquiry)
    {
        return new EnquiryDTO
        {
            Id = enquiry.Id,
            EnquiryMessage = enquiry.EnquiryMessage,
            ProjectRoleId = enquiry.ProjectRoleId,
            Enquirer = enquiry.Enquirer?.ToInformationDTO(),
            ProjectManager = enquiry.ProjectManager?.ToInformationDTO(),
            Shortlisted = enquiry.Shortlisted,
            ProjectId = enquiry.ProjectRole?.ProjectId
        };
    }

    public static ChatDTO ToEnquiriesReceivedChatDTO(this Enquiry enquiry)
    {
        return new ChatDTO
        {
            ChatType = ChatType.Enquiry,
            ChatIdType = enquiry.Id,
            ChatMembers = [enquiry.Enquirer!.ToChatMemberDTO()]
        };
    }
}
