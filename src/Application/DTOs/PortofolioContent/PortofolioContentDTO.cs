using Domain.Enums;

namespace Application.DTOs.PortofolioContentDTOs;

public class PortofolioContentDTO
{
    public int Id { get; set; }
    public string? Description { get; set; }
    public string? Name { get; set; }
    public required FileType FileType { get; set; }
    public required int Order { get; set; }
    public required Uri? Uri { get; set; }
}
