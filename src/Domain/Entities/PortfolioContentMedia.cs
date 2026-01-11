using Domain.Enums;

namespace Domain.Entities;

public class PortfolioContentMedia
{
    public int Id { get; set; }
    public required string Uri { get; set; }
    public required MediaType MediaType { get; set; }
    public required int Order { get; set; }
    public int UserId { get; set; }
    public User? User { get; set; }
}