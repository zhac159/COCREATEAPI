using Application.DTOs.Chat;
using Application.Extensions;
using Application.Interfaces;
using Domain.Entities;
using Domain.Enums;
using Domain.Interfaces;

namespace Application.Services;

public class ChatService(
    IChatRepository chatRepository,
    ICurrentUserContextService currentUserContext
) : IChatService
{
    public async Task<ChatDTO> CreateAsync(CreateChatParams createChatParams)
    {
        var chatMemberships = new List<ChatMembership>
        {
            new() { UserId = currentUserContext.GetUserId() }
        };

        if (createChatParams.AdditionalUserId.HasValue)
        {
            chatMemberships.Add(new() { UserId = createChatParams.AdditionalUserId.Value });
        }

        var chat = new Chat
        {
            ChatType = createChatParams.ChatType,
            ChatTypeId = createChatParams.ChatTypeId,
            GroupChatName = createChatParams.GroupChatName,
            ChatMemberships = chatMemberships
        };

        await chatRepository.CreateAsync(chat);

        return chat.ToDTO();
    }
}
