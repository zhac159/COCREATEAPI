using Application.DTOs.MessageDTOs;
using Domain.Entities;

namespace Application.Extensions;

public static class MessageExtensions
{
    public static MessageDTO ToDTO(this Message message)
    {
        return new MessageDTO
        {
            Id = message.Id,
            SenderId = message.SenderId,
            ChatId = message.ChatId,
            ChatType = message.ChatType,
            Content = message.Content,
            Uri = message.Uri,
            MediaType = message.MediaType,
            Nonce =  message.Nonce,
            Date = message.Date
        };
    }
}