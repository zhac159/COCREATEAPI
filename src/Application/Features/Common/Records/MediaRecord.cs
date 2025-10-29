using Infrastructure.Enums;

namespace Application.Features.Common.Records;

public record MediaRecord
{
    public required string Uri { get; set; }
    public required MediaType MediaType { get; set; }
    public int Id { get; set; }

    public static MediaRecord FromUri(string uri, MediaType mediaType) =>
        new()
        {
            Uri = uri,
            MediaType = mediaType,
            Id = 0,
        };
}

public record UpdateMediaRecord : MediaRecord
{
    public new int? Id { get; set; }
    public int Order { get; set; }
}
