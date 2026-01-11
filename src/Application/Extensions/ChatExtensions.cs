using Application.DTOs.Chat;
using Domain.Entities;

namespace Application.Extensions;

public static class ChatExtensions
{
    public static ChatDTO ToDTO(this Chat chat)
    {
        return new ChatDTO
        {
            Id = chat.Id,
            ChatType = chat.ChatType,
            ChatTypeId = chat.ChatTypeId,
            ChatMembers = [.. chat.ChatMemberships.Select(cm => cm.User.ToChatMemberDTO())],
            GroupChatName = chat.GroupChatName,
        };
    }
}
