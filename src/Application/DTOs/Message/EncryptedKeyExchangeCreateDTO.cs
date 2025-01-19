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
    public required int TargetUserId { get; set; }

    [Required]
    public required int ChatId { get; set; }

    public EncryptedKeyExchange ToEntity()
    {
        return new EncryptedKeyExchange
        {
            Id = Guid.NewGuid(),
            PublicKey = PublicKey,
            Nonce = Nonce,
            EncryptedSymmetricKey = EncryptedSymmetricKey,
            ChatId = ChatId,
            TargetUserId = TargetUserId
        };
    }
}
