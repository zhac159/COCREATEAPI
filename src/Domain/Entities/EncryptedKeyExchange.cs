namespace Domain.Entities
{
    public class EncryptedKeyExchange
    {
        public required Guid Id { get; set; }
        public required string Nonce { get; set; }
        public required string PublicKey { get; set; }
        public required string EncryptedSymmetricKey { get; set; }
        public required int TargetUserId { get; set; }
        public required int ChatId { get; set; }
    }
}
