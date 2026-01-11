using Domain.Enums;

namespace Domain.Entities;

public class ExperienceMedia
{
    public int Id { get; set; }
    public required string Uri { get; set; }
    public required MediaType MediaType { get; set; }
    public required int Order { get; set; }
    public int ExperienceId { get; set; }
    public Experience? Experience { get; set; }
}