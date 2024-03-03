using Application.DTOs.MediaDTOs;
using Domain.Enums;

namespace Application.DTOs.PortofolioContentDTOs;

public class PortofolioContentDTO
{
    public int Id { get; set; }
    public required string Description { get; set; }
    public required SkillType SkillType { get; set; }
    public List<MediaDTO> Medias { get; set; } = new List<MediaDTO>();
}
