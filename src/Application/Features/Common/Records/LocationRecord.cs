using Infrastructure.Entities;
using NetTopologySuite.Geometries;

namespace Application.Features.Common.Records;

public record LocationRecord
{
    public required double Longitude { get; set; }

    public required double Latitude { get; set; }

    public required string Address { get; set; }

    public static LocationRecord FromUser(User user) =>
        new()
        {
            Longitude = user.Location?.X ?? 0,
            Latitude = user.Location?.Y ?? 0,
            Address = user.Address ?? "",
        };

    public Point ToPoint() => new(Longitude, Latitude) { SRID = 4326 };
}
