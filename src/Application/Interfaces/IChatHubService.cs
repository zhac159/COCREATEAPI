using Application.DTOs.EnquiryDTOs;
using Application.DTOs.MessageDTOs;
using Domain.Entities;

namespace Application.Interfaces;

public interface IChatHubService
{
    Task SendMessageAsync(MessageCreateDTO message);
    Task SendNewEnquiry(EnquiryDTO enquiryDTO);
    Task SendNewShortlist(Enquiry enquiry);
    Task SendCompleteProject(Project project);
}
