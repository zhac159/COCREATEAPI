namespace Application.DTOs.AssetDTOs;

public class AssetUpdateWrapperDTO 
{
    public required AssetUpdateDTO AssetUpdateDTO { get; set; }
    public List<IFormFile>? MediaFiles { get; set; }
}