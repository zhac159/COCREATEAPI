namespace Application.DTOs.AssetDTOs;

public class AssetCreateWrapperDTO 
{
    public required AssetCreateDTO Asset { get; set; }
    public required IFormFile MediaFile { get; set; }
}