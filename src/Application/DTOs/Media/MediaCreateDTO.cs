using Domain.Entities;
using Domain.Enums;

namespace Application.DTOs.MediaDTOs;

public class MediaCreateDTO
{
    public required string Uri { get; set; }
    public required MediaType MediaType { get; set; }

    public AssetMedia ToAssetMediaEntity(int order)
    {
        return new AssetMedia
        {
            Uri = Uri,
            MediaType = MediaType,
            Order = order
        };
    }

    public PortofolioContentMedia ToPortofolioContentMediaEntity(int order)
    {
        return new PortofolioContentMedia
        {
            Uri = Uri,
            MediaType = MediaType,
            Order = order
        };
    }

    public ProjectMedia ToProjectMediaEntity( int order)
    {
        return new ProjectMedia
        {
            Uri = Uri,
            MediaType = MediaType,
            Order = order
        };
    }

    public ProjectRoleMedia ToProjectRoleMediaEntity( int order)
    {
        return new ProjectRoleMedia
        {
            Uri = Uri,
            MediaType = MediaType,
            Order = order
        };
    }
}
