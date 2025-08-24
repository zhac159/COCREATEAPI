using System.ComponentModel.DataAnnotations;
using Domain.Entities;
using Domain.Enums;

namespace Application.DTOs.MediaDTOs;

public class MediaCreateDTO
{
    [Required]
    public required string Uri { get; set; }
    [Required]
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

    public PortfolioContentMedia ToPortfolioContentMediaEntity(int order)
    {
        return new PortfolioContentMedia
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

    public ExperienceMedia ToExperienceMediaEntity( int order)
    {
        return new ExperienceMedia
        {
            Uri = Uri,
            MediaType = MediaType,
            Order = order
        };
    }
}
