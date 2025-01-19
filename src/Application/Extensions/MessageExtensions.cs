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
            Salt = message.Salt,
            ChatId = message.ChatId,
            SenderId = message.SenderId,
            Content = message.Content,
            Uri = message.Uri,
            Date = message.Date,
            ReplyMessageId = message.ReplyMessageId
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
