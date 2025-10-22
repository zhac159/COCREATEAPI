using System.Collections.Concurrent;
using Application.Features.Hubs.Notifications.Chat.SendMessage;
using Mediator;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;

namespace Application.Features.Hubs.Notifications;

[Authorize]
public class NotificationHub(IMediator mediator) : Hub
{
    public ConcurrentDictionary<string, bool> connectedUsers = [];

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

    public async Task<SendMessageResponse> SendMessage(SendMessageRequest request)
    {
        var response = await mediator.Send(request);
        return response;
    }
}
