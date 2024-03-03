using Application.DTOs.MediaDTOs;
using Domain.Entities;
using Domain.Enums;

namespace Application.DTOs.PortofolioContentDTOs;

public class PortofolioContentUpdateDTO
{
    public int Id { get; set; }
    public string? Description { get; set; }
    public SkillType? SkillType { get; set; }
    public int? Order { get; set; }
    public List<MediaUpdateDTO>? Medias { get; set; }

    public PortofolioContent ToPortofolioContentEntity()
    {
        return new PortofolioContent
        {
            Id = Id,
            Description = Description ?? string.Empty,
            SkillType = SkillType ?? default,
            CreatedAt = DateTime.Now,
            Order = Order ?? 0,
            Medias =
                Medias
                    ?.Select((media, index) => media.ToPortofolioContentMediaEntity(index))
                    .ToList() ?? new List<PortofolioContentMedia>()
        };
    }
}
