using Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;

[Authorize]
public class ChatHubService : Hub, IChatHubService
{
    private readonly IHubContext<ChatHubService> hubContext;

    public ChatHubService(IHubContext<ChatHubService> hubContext)
    {
        this.hubContext = hubContext;
    }

    public async Task SendMessage(string message)
    {
        await hubContext.Clients.User("2").SendAsync("ReceiveMessage", message);
    }
}