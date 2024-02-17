using Domain.Enums;

namespace Application.DTOs.PortofolioContentDTOs;

public class PortofolioContentUpdateDTO
{
    public int Id { get; set; }
    public string Description { get; set; } = null!;
    public string Name { get; set; } = null!;
    public int? Order { get; set; }
}