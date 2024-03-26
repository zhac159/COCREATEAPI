using Domain.Enums;

namespace Domain.Entities
{
    public class Message
    {
        public Guid Id { get; set; }
        public int SenderId { get; set; }
        public string? Content { get; set; }
        public string? Uri { get; set; }
        public MediaType? MediaType { get; set; }
        public DateTime Date { get; set; }
        public ChatType ChatType { get; set; }
        public int ChatId { get; set; }
    }
}
