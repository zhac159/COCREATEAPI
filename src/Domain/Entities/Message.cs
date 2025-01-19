using Domain.Enums;

namespace Domain.Entities
{
    public class Message
    {
        public required Guid Id { get; set; }
        public required string Salt { get; set; }
        public required int ChatId { get; set; }
        public required int SenderId { get; set; }
        public required int TargetUserId { get; set; }
        public string? Content { get; set; }
        public string? Uri { get; set; }
        public DateTime Date { get; set; }
        public Guid? ReplyMessageId { get; set; }
    }
}
