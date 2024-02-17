using Application.DTOs.PortofolioContentDTOs;
using Application.Interfaces;
using Domain.Entities;

namespace Application.Extensions;

public static class PortofolioContentExtensions
{
    public static PortofolioContentDTO ToDTO(this PortofolioContent portofolioContent)
    {
        return new PortofolioContentDTO
        {
            Id = portofolioContent.Id,
            Name = portofolioContent.Name,
            Description = portofolioContent.Description,
            Order = portofolioContent.Order,
            Uri = portofolioContent.Uri ?? null
        };
    }

    public static void UpdateFromDTO(this PortofolioContent portofolioContent, PortofolioContentUpdateDTO portofolioContentUpdateDTO)
    {
        portofolioContent.Name = portofolioContentUpdateDTO.Name ?? portofolioContent.Name;
        portofolioContent.Description = portofolioContentUpdateDTO.Description ?? portofolioContent.Description;
        portofolioContent.Order = portofolioContentUpdateDTO.Order ?? portofolioContent.Order;
    }
}