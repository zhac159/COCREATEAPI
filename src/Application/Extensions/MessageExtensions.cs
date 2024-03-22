using Application.DTOs.EnquiryDTOs;
using Application.DTOs.MediaDTOs;
using Domain.Entities;

namespace Application.Extensions;


public static class MessageExtensions
{
    public static EnquiryMessageDTO ToDTO(this EnquiryMessage message)
    {
        return new EnquiryMessageDTO
        {
            Id = message.Id,
            SenderId = message.SenderId,
            EnquiryId = message.EnquiryId,
            Message = message.Message,
            Uri = message.Uri,
            MediaType = message.MediaType,
            Date = message.Date
        };
    }
}
