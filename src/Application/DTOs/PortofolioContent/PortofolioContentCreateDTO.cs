using System.ComponentModel.DataAnnotations;
using Application.DTOs.MediaDTOs;
using Domain.Entities;
using Domain.Enums;

namespace Application.DTOs.PortofolioContentDTOs;

public class PortofolioContentCreateDTO
{
    [Required]
    public required string Description { get; set; } = "";
    public required int Order { get; set; }

    [Required]
    public required SkillType SkillType { get; set; }

    [Required]
    public required List<MediaCreateDTO> Medias { get; set; } = [];

    public PortofolioContent ToEntity(int userId)
    {
        return new PortofolioContent
        {
            Description = Description,
            SkillType = SkillType,
            Order = Order,
            CreatedAt = DateTime.UtcNow,
            UserId = userId,
            Medias = Medias.Select((m, order) => m.ToPortofolioContentMediaEntity(order)).ToList()
        };
    }
}
