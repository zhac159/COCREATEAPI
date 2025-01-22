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

    public async Task KeyExchangeAsync(
        List<EncryptedKeyExchangeCreateDTO> encryptedKeyExchangeCreateDTO
    )
    {
        var encryptedKeyExchanges = encryptedKeyExchangeCreateDTO
            .Select(e => e.ToEntity())
            .ToList();

        await messageStorageService.AddEncryptedKeyExchangeAsync(encryptedKeyExchanges);
    }

    public async Task SendMessageAsync(MessageCreateDTO messageCreateDTO)
    {
        var userId = GetUserId();

        var messages = messageCreateDTO.ToEntity(userId);

        await messageStorageService.AddMessagesAsync(messages);

        foreach (var message in messages)
        {
            var messagesDTO = new List<MessageDTO> { message.ToDTO() };
            await hubContext
                .Clients.User(message.TargetUserId.ToString())
                .SendAsync("ReceiveMessages", messagesDTO);
        }
    }

    public async Task GetMessagesAsync()
    {
        var userId = GetUserId();

        var messages = await messageStorageService.GetMessagesAsync(userId);

        var messagesDTO = messages.Select(m => m.ToDTO());

        await Clients.Caller.SendAsync("ReceiveMessages", messagesDTO);
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

    public async Task SendMessageReactionAsync(MessageReactionCreateDTO messageReactionCreateDTO)
    {
        var userId = GetUserId();

        var messageReaction = messageReactionCreateDTO.ToEntity(userId);

        if (messageReactionCreateDTO.ChatType == ChatType.Project)
        {
            await SendGroupReaction(messageReaction, messageReactionCreateDTO.ChatId);
            return;
        }

        await messageStorageService.AddMessageReactionAsync(
            messageReaction,
            messageReactionCreateDTO.TargetId
        );

        var messageReactionDTO = new List<MessageReactionDTO> { messageReaction.ToDTO() };

        await hubContext
            .Clients.User(messageReactionCreateDTO.TargetId.ToString())
            .SendAsync("ReceiveMessagesReactions", messageReactionDTO);
    }

    private async Task SendGroupReaction(MessageReaction messageReaction, string chatId)
    {
        var recipientIds = await messageStorageService.GetChatMemebersAsync(chatId);

        if (recipientIds == null || !recipientIds.Contains(messageReaction.UserId))
        {
            throw new UnauthorizedAccessException();
        }

        foreach (var recipientId in recipientIds)
        {
            if (recipientId.ToString() != Context.UserIdentifier)
            {
                await messageStorageService.AddMessageReactionAsync(messageReaction, recipientId);

                var messageReactionDTO = new List<MessageReactionDTO> { messageReaction.ToDTO() };

                await hubContext
                    .Clients.User(recipientId.ToString())
                    .SendAsync("ReceiveMessagesReactions", messageReactionDTO);
            }
        }
    }

    public async Task AknowledgeEncryptedKeyExchangeAsync(IEnumerable<Guid> encryptedKeyExchangeIds)
    {
        var userId = GetUserId();

        await messageStorageService.DeleteEncryptedKeyExchangeAsync(
            encryptedKeyExchangeIds,
            userId
        );
    }

    public async Task GetMessagesReactionsAsync()
    {
        var userId = GetUserId();

        var messagesReactions = await messageStorageService.GetMessagesReactionsAsync(userId);

        var messagesReactionsDTO = messagesReactions.Select(mr => mr.ToDTO());

        await Clients.Caller.SendAsync("ReceiveMessagesReactions", messagesReactionsDTO);
    }

    public async Task AknowledgeMessagesAsync(IEnumerable<Guid> messageIds)
    {
        var userId = GetUserId();

        await messageStorageService.DeleteMessageAsync(messageIds, userId);
    }

    public async Task SendNewEnquiry(EnquiryDTO enquiryDTO)
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

    public async Task SendCompleteProject(Project project)
    {
        var recipientIds = project
            .ProjectRoles.Where(pr => pr.AssigneeId != null)
            .Select(pr => pr.AssigneeId!.Value)
            .ToList();

        foreach (var recipientId in recipientIds)
        {
            await hubContext
                .Clients.User(recipientId.ToString())
                .SendAsync("ReceiveCompleteProject", project.Id);
        }
    }

    public async Task AknowledgeMessageReactionsAsync()
    {
        var userId = GetUserId();

        await messageStorageService.DeleteMessageReactionAsync(userId);
    }

    private int GetUserId()
    {
        return int.Parse(Context.UserIdentifier ?? throw new Exception("User id is null"));
    }
}
