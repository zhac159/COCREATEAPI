namespace Application.DTOs.AssetDTOs;

public class AssetUpdateWrapperDTO 
{
    public required AssetUpdateDTO AssetUpdateDTO { get; set; }
    public IFormFile? MediaFile { get; set; }
}