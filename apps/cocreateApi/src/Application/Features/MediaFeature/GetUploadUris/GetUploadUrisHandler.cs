using Infrastructure.Enums;
using Infrastructure.Interfaces;
using Mediator;

namespace Application.Features.MediaFeature.GetUploadUris;

public sealed record GetUploadUrisRequest : IQuery<List<GetUploadUrisResponse>>
{
    public required List<MediaType> MediaTypes { get; set; }
    public required MediaCategory MediaCategory { get; set; }
}

public sealed class GetUploadUrisRequestHandler(IStorageService storageService)
    : IQueryHandler<GetUploadUrisRequest, List<GetUploadUrisResponse>>
{
    public ValueTask<List<GetUploadUrisResponse>> Handle(
        GetUploadUrisRequest query,
        CancellationToken cancellationToken
    )
    {
        var blobPrefix = query.MediaCategory.ToString().ToLowerInvariant();

        var uploadUris = query
            .MediaTypes.Select(mediaType => new GetUploadUrisResponse
            {
                MediaType = mediaType,
                UploadUri = storageService.GenerateUploadSasUri(blobPrefix, mediaType),
            })
            .ToList();

        return ValueTask.FromResult(uploadUris);
    }
}
