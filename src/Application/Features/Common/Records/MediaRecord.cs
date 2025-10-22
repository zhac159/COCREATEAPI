using Infrastructure.Entities;
using Infrastructure.Enums;

namespace Application.Features.Common.Records;

public record MediaRecord
{
    public int Id { get; set; }
    public required string Uri { get; set; }
    public required MediaType MediaType { get; set; }

    public static MediaRecord FromPorfolioContentMedia(PortflioContentMedia media) =>
        new()
        {
            Id = media.Id,
            Uri = media.Uri,
            MediaType = media.MediaType,
        };

    public static MediaRecord FromUri(string uri, MediaType mediaType) =>
        new() { Uri = uri, MediaType = mediaType };
}
