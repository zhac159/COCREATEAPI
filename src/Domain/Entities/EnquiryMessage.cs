using Domain.Enums;

namespace Domain.Entities
{
    public class EnquiryMessage
    {
        public Guid Id { get; set; }
        public int SenderId { get; set; }
        public User? Sender { get; set; }
        public string? Message { get; set; }
        public string? Uri { get; set; }
        public MediaType? MediaType { get; set; }
        public DateTime Date { get; set; }
        public int EnquiryId { get; set; }
        public Enquiry? Enquiry { get; set; }
    }
}
