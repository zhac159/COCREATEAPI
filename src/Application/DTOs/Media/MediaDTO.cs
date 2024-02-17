using Domain.Enums;

namespace Application.DTOs.MediaDTOs;

public class MediaDTO
{
    public int Id { get; set; }
    public required string Uri { get; set; }
    public required MediaType MediaType { get; set; }
}
