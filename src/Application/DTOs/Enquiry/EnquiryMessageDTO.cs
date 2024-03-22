using Domain.Enums;

namespace Application.DTOs.EnquiryDTOs;

public class EnquiryMessageDTO
{
    public required Guid Id { get; set; }
    public required int SenderId { get; set; }
    public required int EnquiryId { get; set; }
    public string? Message { get; set; }
    public string? Uri { get; set; }
    public MediaType? MediaType { get; set; }
    public required DateTime Date { get; set; }
}