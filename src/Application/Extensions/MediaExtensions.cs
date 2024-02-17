using Application.DTOs.AssetDTOs;
using Application.DTOs.EnquiryDTOs;
using Application.DTOs.MediaDTOs;
using Domain.Entities;

namespace Application.Extensions;

public static class MediaExtensions
{
    public static MediaDTO ToDTO(this AssetMedia media)
    {
        return new MediaDTO
        {
            Id = media.Id,
            Uri = media.Uri,
            MediaType = media.MediaType
        };
    }

    public static void UpdateFromDTO(this AssetMedia media, MediaUpdateDTO mediaUpdateDto, int order)
    {
        media.Uri = mediaUpdateDto.Uri;
        media.MediaType = mediaUpdateDto.MediaType;
        media.Order = order;
    }
}
