using System.ComponentModel.DataAnnotations.Schema;
using Domain.Enums;

namespace Domain.Entities;

public class PortofolioContent
{
    public int Id { get; set; }
    public required string Description { get; set; }
    public required string Name { get; set; }
    public required string FileSrc { get; set; }
    public required int Order { get; set; }
    public int UserId { get; set; }
    public User? User { get; set; }

    [NotMapped]
    public Uri? Uri { get; set; }
}
