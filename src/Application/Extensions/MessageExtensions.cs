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
            TargetId = message.TargetId,
            ChatType = message.ChatType,
            Content = message.Content,
            Uri = message.Uri,
            MediaType = message.MediaType,
            Date = message.Date
        };
    }

    public static EncryptedKeyExchangeDTO ToDTO(this EncryptedKeyExchange encryptedKeyExchange)
    {
        return new EncryptedKeyExchangeDTO
        {
            Id = encryptedKeyExchange.Id,
            Nonce = encryptedKeyExchange.Nonce,
            PublicKey = encryptedKeyExchange.PublicKey,
            EncryptedSymmetricKey = encryptedKeyExchange.EncryptedSymmetricKey,
            TargetId = encryptedKeyExchange.TargetId,
            SenderId = encryptedKeyExchange.SenderId,
            GroupChatId = encryptedKeyExchange.GroupChatId,
            ChatType = encryptedKeyExchange.ChatType
        };
    }
}
