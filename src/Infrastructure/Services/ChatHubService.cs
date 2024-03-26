using System.Collections.Concurrent;
using Application.DTOs.MessageDTOs;
using Application.Extensions;
using Application.Interfaces;
using Domain.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;

[Authorize]
public class ChatHubService : Hub, IChatHubService
{
    private readonly IHubContext<ChatHubService> hubContext;
    public static ConcurrentDictionary<string, bool> connectedUsers =
        new ConcurrentDictionary<string, bool>();

    private readonly IMessageStorageService messageStorageService;

    public ChatHubService(
        IHubContext<ChatHubService> hubContext,
        IMessageStorageService messageStorageService
    )
    {
        this.hubContext = hubContext;
        this.messageStorageService = messageStorageService;
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

    public async Task SendMessageAsync(MessageCreateDTO messageCreateDTO)
    {
        var userId = int.Parse(Context.UserIdentifier ?? throw new Exception("User id is null"));

        var recipientIds = await messageStorageService.GetChatMemebersAsync(
            messageCreateDTO.ChatId,
            messageCreateDTO.ChatType
        );

        if (recipientIds == null || !recipientIds.Contains(userId))
        {
            throw new UnauthorizedAccessException();
        }

        var message = messageCreateDTO.ToEntity(userId);

        foreach (var recipientId in recipientIds)
        {
            if (recipientId.ToString() != Context.UserIdentifier)
            {
                await messageStorageService.AddMessageAsync(message, recipientId);

                var messageDTO = new List<MessageDTO> { message.ToDTO() };

                await hubContext
                    .Clients.User(recipientId.ToString())
                    .SendAsync("ReceiveMessages", messageDTO);
            }
        }
    }

    public async Task GetMessagesAsync()
    {
        var userId = int.Parse(Context.UserIdentifier ?? throw new Exception("User id is null"));

        var messages = await messageStorageService.GetMessagesAsync(userId);

        var messagesDTO = messages.Select(m => m.ToDTO());

        await Clients.Caller.SendAsync("ReceiveMessages", messagesDTO);
    }

    public async Task AknowledgeMessageAsync(IEnumerable<Guid> messageIds)
    {
        var userId = int.Parse(Context.UserIdentifier ?? throw new Exception("User id is null"));

        await messageStorageService.DeleteMessageAsync(messageIds, userId);
    }
}
