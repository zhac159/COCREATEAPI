using Domain.Entities;
using Domain.Enums;

namespace Application.DTOs.MessageDTOs;

public class EncryptedKeyExchangeCreateDTO
{
    public required string Nonce { get; set; }
    public required string PublicKey { get; set; }
    public required string EncryptedSymmetricKey { get; set; }
    public int TargetId { get; set; }
    public ChatType ChatType { get; set; }

    public EncryptedKeyExchange ToEntity(int userId)
    {
        return new EncryptedKeyExchange
        {
            Id = Guid.NewGuid(),
            PublicKey = PublicKey,
            Nonce = Nonce,
            EncryptedSymmetricKey = EncryptedSymmetricKey,
            TargetId = TargetId,
            SenderId = userId,
            ChatType = ChatType
        };
    }
}
