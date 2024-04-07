using Domain.Enums;

namespace Application.DTOs.MessageDTOs;

public class EncryptedKeyExchangeDTO
{
    public required Guid Id { get; set; }
    public required string Nonce { get; set; }
    public required string PublicKey { get; set; }
    public required string EncryptedSymmetricKey { get; set; }
    public int TargetId { get; set; }
    public int? GroupChatId { get; set; }
    public int SenderId { get; set; }
    public ChatType ChatType { get; set; }
}
