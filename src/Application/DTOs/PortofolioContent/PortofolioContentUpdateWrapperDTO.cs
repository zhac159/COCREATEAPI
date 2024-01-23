namespace Application.DTOs.PortofolioContentDTOs;

public class PortofolioContentUpdateWrapperDTO
{
    public required PortofolioContentUpdateDTO PortofolioContentUpdateDTO { get; set; }
    public IFormFile? MediaFile { get; set; }
}