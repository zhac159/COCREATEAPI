using Domain.Enums;

namespace Domain.Entities;

public class PortofolioContent
{
    public int Id { get; set; }
    public required string Description { get; set; } = "";
    public required string Title { get; set; }
    public required string FileSrc { get; set; }
    public required FileType FileType { get; set; }
    public required int Order { get; set; }
    public required int UserId { get; set; }
    public required User User { get; set; }
}
