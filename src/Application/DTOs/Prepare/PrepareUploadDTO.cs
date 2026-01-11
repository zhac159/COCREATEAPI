using Domain.Enums;

namespace Application.DTOs.PrepareDTOs;

public class PrepareUploadDTO
{
    public required EntityType Entity { get; set; }
    public required MediaType MediaType { get; set; }
}
