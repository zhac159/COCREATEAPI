namespace Application.DTOs.PortofolioContentDTOs;

public class PortofolioContentCreateWrapperDTO 
{
    public required PortofolioContentCreateDTO PortofolioContent { get; set; }
    public required IFormFile MediaFile { get; set; }
}