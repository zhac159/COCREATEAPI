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

    public static MediaDTO ToDTO(this PortofolioContentMedia media)
    {
        return new MediaDTO
        {
            Id = media.Id,
            Uri = media.Uri,
            MediaType = media.MediaType
        };
    }

    public static MediaDTO ToDTO(this ProjectMedia media)
    {
        return new MediaDTO
        {
            Id = media.Id,
            Uri = media.Uri,
            MediaType = media.MediaType
        };
    }

    public static MediaDTO ToDTO(this ProjectRoleMedia media)
    {
        return new MediaDTO
        {
            Id = media.Id,
            Uri = media.Uri,
            MediaType = media.MediaType
        };
    }

    public static void UpdateFromDTO(
        this AssetMedia media,
        MediaUpdateDTO mediaUpdateDto,
        int order
    )
    {
        media.Uri = mediaUpdateDto.Uri;
        media.MediaType = mediaUpdateDto.MediaType;
        media.Order = order;
    }

    public static void UpdateFromDTO(
        this PortofolioContentMedia media,
        MediaUpdateDTO mediaUpdateDto,
        int order
    )
    {
        media.Uri = mediaUpdateDto.Uri;
        media.MediaType = mediaUpdateDto.MediaType;
        media.Order = order;
    }

    public static void UpdateFromDTO(
        this ProjectMedia media,
        MediaUpdateDTO mediaUpdateDto,
        int order
    )
    {
        media.Uri = mediaUpdateDto.Uri;
        media.MediaType = mediaUpdateDto.MediaType;
        media.Order = order;
    }

    public static void UpdateFromDTO(
        this ProjectRoleMedia media,
        MediaUpdateDTO mediaUpdateDto,
        int order
    )
    {
        media.Uri = mediaUpdateDto.Uri;
        media.MediaType = mediaUpdateDto.MediaType;
        media.Order = order;
    }
}
