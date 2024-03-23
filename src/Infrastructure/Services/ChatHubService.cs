using System.Collections.Concurrent;
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
    public static ConcurrentDictionary<string, bool> connectedUsers =
        new ConcurrentDictionary<string, bool>();

    public ChatHubService(
        IHubContext<ChatHubService> hubContext,
        IEnquiryMessageRepository enquiryMessageRepository
    )
    {
        this.hubContext = hubContext;
        this.enquiryMessageRepository = enquiryMessageRepository;
    }

    public override async Task OnConnectedAsync()
    {
        var userId = Context.UserIdentifier;
        if (userId != null)
        {
            connectedUsers.TryAdd(userId, true);
        }
        await base.OnConnectedAsync();
    }

    public override async Task OnDisconnectedAsync(Exception? exception)
    {
        var userId = Context.UserIdentifier;
        if (userId != null)
        {
            connectedUsers.TryRemove(userId, out _);
        }
        await base.OnDisconnectedAsync(exception);
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
}
