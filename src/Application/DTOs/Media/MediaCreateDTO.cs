using Domain.Entities;
using Domain.Enums;

namespace Application.DTOs.MediaDTOs;

public class MediaCreateDTO
{
    public required string Uri { get; set; }
    public required MediaType MediaType { get; set; }

    public AssetMedia ToEntity(int order)
    {
        return new AssetMedia
        {
            Uri = Uri,
            MediaType = MediaType,
            Order = order
        };
    }
}

