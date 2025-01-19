using System.ComponentModel.DataAnnotations;

namespace Application.DTOs.MessageDTOs;

public class EncryptedKeyExchangeDTO
{
    [Required]
    public required Guid Id { get; set; }

    [Required]
    public required string Nonce { get; set; }

    [Required]
    public required string PublicKey { get; set; }

    [Required]
    public required string EncryptedSymmetricKey { get; set; }

    [Required]
    public required int ChatId { get; set; }
}
