using System.ComponentModel.DataAnnotations;
using Domain.Entities;
using Domain.Enums;

namespace Application.DTOs.MessageDTOs;

public class EncryptedKeyExchangeCreateDTO
{
    [Required]
    public required string Nonce { get; set; }

    [Required]
    public required string PublicKey { get; set; }

    [Required]
    public required string EncryptedSymmetricKey { get; set; }

    [Required]
    public int TargetId { get; set; }

    [Required]
    public ChatType ChatType { get; set; }

    [Required]
    public required string ChatId { get; set; }

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
            ChatId = ChatId,
        };
    }
}
