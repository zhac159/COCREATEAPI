using Application.DTOs.EnquiryDTOs;
using Application.Interfaces;
using Domain.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;

[Authorize]
public class ChatHubService : Hub, IChatHubService
{
    private readonly IHubContext<ChatHubService> hubContext;
    private readonly IEnquiryMessageRepository enquiryMessageRepository;

    public ChatHubService(
        IHubContext<ChatHubService> hubContext,
        IEnquiryMessageRepository enquiryMessageRepository
    )
    {
        this.hubContext = hubContext;
        this.enquiryMessageRepository = enquiryMessageRepository;
    }

    public async Task SendMessageAsync(string message)
    {
        await hubContext.Clients.User("2").SendAsync("ReceiveMessage", message);
    }

    public async Task SendNewEnquiryAsync(EnquiryDTO message, int projectManagerId)
    {
        await hubContext
            .Clients.User(projectManagerId.ToString())
            .SendAsync("ReceiveNewEnquiry", message);
    }

    public async Task SendEnquiryMessageAsync(EnquiryMessageDTO message, int receiverId)
    {
        await hubContext
            .Clients.User(receiverId.ToString())
            .SendAsync("ReceiveEnquiryMessage", message);
    }

    // public async Task Acknowledge(int messageId)
    // {

    //     if (Context.User?.Identity == null)
    //     {
    //         throw new UnauthorizedAccessException("User is not authenticated");
    //     }

    //     var message = await enquiryMessageRepository.GetByIdAsync(messageId);


    //     await enquiryMessageRepository.DeleteAsync(messageId);
    // }
}
