using Domain.Enums;

namespace Domain.Queries;

public class GetMatchingProjectRolesAsyncQuery
{
    public double Latitude { get; set; }
    public double Longitude { get; set; }
    public double Distance { get; set; }
    public required List<SkillType> SkillTypes { get; set; }
    public int Effort { get; set; }
    public int UserId { get; set; }
}
