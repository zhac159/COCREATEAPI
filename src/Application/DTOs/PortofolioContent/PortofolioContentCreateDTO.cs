using Domain.Entities;
using Domain.Enums;

namespace Application.DTOs.PortofolioContentDTOs;

public class PortofolioContentCreateDTO {
    public required string Description { get; set; }
    public required string Name { get; set; }
    public required FileType FileType { get; set; }
    public required int Order { get; set; }

    public PortofolioContent ToEntity(string fileSrc, int userId)
    {
        return new PortofolioContent
        {
            Name = Name,
            Description = Description,
            Order = Order,
            FileSrc = fileSrc,
            FileType = FileType,
            UserId = userId
        };
    }
}