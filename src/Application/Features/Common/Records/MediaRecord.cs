using Infrastructure.Enums;

namespace Application.Features.Common.Records;

public abstract record MediaRecordBase
{
    public required string Uri { get; set; }
    public required MediaType MediaType { get; set; }
}

public record MediaRecord : MediaRecordBase
{
    public int Id { get; set; }

    public static MediaRecord FromUri(string uri, MediaType mediaType) =>
        new()
        {
            Uri = uri,
            MediaType = mediaType,
            Id = 0,
        };
}

public record CreateMediaRecord : MediaRecordBase;

public record UpdateMediaRecord : MediaRecordBase
{
    public int? Id { get; set; }
}
