using System.ComponentModel.DataAnnotations;
using Domain.Entities;
using Domain.Enums;

namespace Application.DTOs.MediaDTOs;

public class MediaUpdateDTO
{
    public int Id { get; set; }

    [Required]
    public required string Uri { get; set; }

    [Required]
    public required MediaType MediaType { get; set; }

    public AssetMedia ToAssetMediaEntity(int order)
    {
        return new AssetMedia
        {
            Id = Id,
            Uri = Uri,
            MediaType = MediaType,
            Order = order
        };
    }

    public PortfolioContentMedia ToPortfolioContentMediaEntity(int order)
    {
        return new PortfolioContentMedia
        {
            Id = Id,
            Uri = Uri,
            MediaType = MediaType,
            Order = order
        };
    }

    public ProjectMedia ToProjectMediaEntity(int order)
    {
        return new ProjectMedia
        {
            Id = Id,
            Uri = Uri,
            MediaType = MediaType,
            Order = order
        };
    }

    public ProjectRoleMedia ToProjectRoleMediaEntity(int order)
    {
        return new ProjectRoleMedia
        {
            Id = Id,
            Uri = Uri,
            MediaType = MediaType,
            Order = order
        };
    }
}
