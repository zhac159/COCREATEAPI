using Domain.Enums;

namespace Domain.Entities
{
    public class EncryptedKeyExchange
    {
        public required Guid Id { get; set; }
        public required string Nonce { get; set; }
        public required string PublicKey { get; set; }
        public required string EncryptedSymmetricKey { get; set; }
        public required int TargetId { get; set; }
        public int? GroupChatId { get; set; }
        public required int SenderId { get; set; }
        public ChatType ChatType { get; set; }
    }
}
