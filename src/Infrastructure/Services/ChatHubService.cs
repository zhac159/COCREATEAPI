using System.Collections.Concurrent;
using Application.DTOs.EnquiryDTOs;
using Application.DTOs.MessageDTOs;
using Application.Extensions;
using Application.Interfaces;
using Domain.Entities;
using Domain.Enums;
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

    public async Task KeyExchangeAsync(EncryptedKeyExchangeCreateDTO encryptedKeyExchangeCreateDTO)
    {
        var userId = GetUserId();

        var encryptedKeyExchange = encryptedKeyExchangeCreateDTO.ToEntity(userId);

        await messageStorageService.AddEncryptedKeyExchangeAsync(encryptedKeyExchange);

        var encryptedKeyExchangeDTO = new List<EncryptedKeyExchangeDTO>
        {
            encryptedKeyExchange.ToDTO()
        };

        await hubContext
            .Clients.User(encryptedKeyExchangeCreateDTO.TargetId.ToString())
            .SendAsync("ReceiveEncryptedKeysExchange", encryptedKeyExchangeDTO);
    }

    public async Task GetEncryptedKeyExchangesAsync()
    {
        var userId = GetUserId();

        var encryptedKeyExchanges = await messageStorageService.GetEncryptedKeyExchangesAsync(
            userId
        );

        var encryptedKeyExchangesDTO = encryptedKeyExchanges.Select(e => e.ToDTO());

        await Clients.Caller.SendAsync("ReceiveEncryptedKeysExchange", encryptedKeyExchangesDTO);
    }

    public async Task SendMessageAsync(MessageCreateDTO messageCreateDTO)
    {
        var userId = GetUserId();

        var message = messageCreateDTO.ToEntity(userId);

        if (message.ChatType == ChatType.Project)
        {
            await SendGroupMessage(message);
            return;
        }
        else
            await messageStorageService.AddMessageAsync(message, messageCreateDTO.TargetId);

        var messageDTO = new List<MessageDTO> { message.ToDTO() };

        await hubContext
            .Clients.User(messageCreateDTO.TargetId.ToString())
            .SendAsync("ReceiveMessages", messageDTO);
    }

    public async Task AknowledgeEncryptedKeyExchangeAsync(IEnumerable<Guid> encryptedKeyExchangeIds)
    {
        var userId = GetUserId();

        await messageStorageService.DeleteEncryptedKeyExchangeAsync(
            encryptedKeyExchangeIds,
            userId
        );
    }

    public async Task GetMessagesAsync()
    {
        var userId = GetUserId();

        var messages = await messageStorageService.GetMessagesAsync(userId);

        var messagesDTO = messages.Select(m => m.ToDTO());

        await Clients.Caller.SendAsync("ReceiveMessages", messagesDTO);
    }

    public async Task AknowledgeMessageAsync(IEnumerable<Guid> messageIds)
    {
        var userId = GetUserId();

        await messageStorageService.DeleteMessageAsync(messageIds, userId);
    }

    private async Task SendGroupMessage(Message message)
    {
        var recipientIds = await messageStorageService.GetChatMemebersAsync(
            message.TargetId,
            message.ChatType
        );

        if (recipientIds == null || !recipientIds.Contains(message.SenderId))
        {
            throw new UnauthorizedAccessException();
        }

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

    public async Task SendNewEnqruiry(EnquiryDTO enquiryDTO)
    {
        await hubContext
            .Clients.User(enquiryDTO.ProjectManager!.UserId.ToString())
            .SendAsync("ReceiveNewEnquiry", enquiryDTO);
    }

    public async Task SendNewShortlist(Enquiry enquiry)
    {
        await hubContext
            .Clients.User(enquiry.EnquirerId.ToString())
            .SendAsync("ReceiveNewShortlist", enquiry.Id);
    }

    private int GetUserId()
    {
        return int.Parse(Context.UserIdentifier ?? throw new Exception("User id is null"));
    }
}
