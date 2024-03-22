using Application.DTOs.EnquiryDTOs;

namespace Application.Interfaces;
public interface IChatHubService
{
    Task SendMessageAsync(string message);
    Task SendNewEnquiryAsync(EnquiryDTO message, int projectManagerId);
    Task SendEnquiryMessageAsync(EnquiryMessageDTO message, int receiverId);
}