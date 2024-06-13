using System.ComponentModel.DataAnnotations;
using Domain.Enums;

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
    public int TargetId { get; set; }
    public int? GroupChatId { get; set; }

    [Required]
    public int SenderId { get; set; }

    [Required]
    public ChatType ChatType { get; set; }
}
