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
            ReplyMessageId = message.ReplyMessageId,
            Date = message.Date,
            ChatId = message.ChatId
        };
    }

    public static EncryptedKeyExchangeDTO ToDTO(this EncryptedKeyExchange encryptedKeyExchange)
    {
        var a = new EncryptedKeyExchangeDTO
        {
            Id = encryptedKeyExchange.Id,
            Nonce = encryptedKeyExchange.Nonce,
            PublicKey = encryptedKeyExchange.PublicKey,
            EncryptedSymmetricKey = encryptedKeyExchange.EncryptedSymmetricKey,
            ChatId = encryptedKeyExchange.ChatId
        };

        return a;
    }

    public static MessageReactionDTO ToDTO(this MessageReaction messageReaction)
    {
        return new MessageReactionDTO
        {
            MessageId = messageReaction.MessageId,
            Multiplier = messageReaction.Multiplier,
            Reaction = messageReaction.Reaction,
            UserId = messageReaction.UserId,
        };
    }
}
