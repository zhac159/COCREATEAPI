using Infrastructure.Entities;
using Infrastructure.Enums;

namespace Application.Features.Common.Records;

public record MediaRecordBase
{
    public required string Uri { get; set; }
    public required MediaType MediaType { get; set; }
    public int Id { get; set; }
}

public record UpdateMediaRecord : MediaRecordBase
{
    public new int? Id { get; set; }
    public int Order { get; set; }
}

public record MediaRecord : MediaRecordBase
{
    public static MediaRecord FromPorfolioContentMedia(PortflioContentMedia media) =>
        new()
        {
            Id = media.Id,
            Uri = media.Uri,
            MediaType = media.MediaType,
        };

    public static MediaRecord FromUri(string uri, MediaType mediaType) =>
        new()
        {
            Uri = uri,
            MediaType = mediaType,
            Id = 0,
        };

    public static MediaRecord FromUpdateMediaRecord(UpdateMediaRecord updateMediaRecord) =>
        new()
        {
            Id = updateMediaRecord.Id ?? 0,
            Uri = updateMediaRecord.Uri,
            MediaType = updateMediaRecord.MediaType,
        };
}
