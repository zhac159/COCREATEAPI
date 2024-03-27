using Domain.Enums;
using NetTopologySuite.Geometries;

namespace Domain.Queries;

public class GetMatchingProjectRolesAsyncQuery
{
    public required Point Location { get; set; }
    public double Distance { get; set; }
    public required List<SkillType> SkillTypes { get; set; }
    public int Effort { get; set; }
    public int UserId { get; set; }
}
