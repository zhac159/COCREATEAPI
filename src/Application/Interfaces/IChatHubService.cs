using Application.DTOs.MessageDTOs;
using Domain.Entities;

namespace Application.Interfaces;
public interface IChatHubService
{
    Task SendMessageAsync(MessageCreateDTO message);
    // Task SendNewEnquiryAsync(EnquiryDTO message, int projectManagerId);
}