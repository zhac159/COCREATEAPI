using Domain.Enums;

namespace Application.DTOs.MessageDTOs;

public class MessageDTO
{
    public required Guid Id { get; set; }
    public required int SenderId { get; set; }
    public required int ChatId { get; set; }
    public ChatType ChatType { get; set; }
    public string? Content { get; set; }
    public string? Uri { get; set; }
    public MediaType? MediaType { get; set; }
    public required string Nonce { get; set; }
    public required DateTime Date { get; set; }
}
