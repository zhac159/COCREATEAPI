using Domain.Entities;
using Domain.Enums;

namespace Application.DTOs.EnquiryDTOs;

public class NewEnquiryMessageCreateDTO
{
    public string? Message { get; set; }
    public string? Uri { get; set; }
    public MediaType? MediaType { get; set; }
    public required DateTime Date { get; set; }
    public EnquiryMessage ToEntity(int userId)
    {
        return new EnquiryMessage { SenderId = userId, Message = Message, Uri = Uri, MediaType = MediaType, Date = Date };
    }
}