using Application.DTOs.PortofolioContentDTOs;

namespace Application.DTOs.UserDtos;

public class UserPortofolioUpdateDTO
{
    public string? AboutYou { get; set; }
    public List<PortofolioContentUpdateDTO>? PortofolioContents { get; set; }   
}
