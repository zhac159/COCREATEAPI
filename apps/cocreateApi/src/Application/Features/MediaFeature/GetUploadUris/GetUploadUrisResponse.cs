using Infrastructure.Enums;

namespace Application.Features.MediaFeature.GetUploadUris;

public class GetUploadUrisResponse
{
    public required MediaType MediaType { get; set; }
    public required string UploadUri { get; set; }
}
