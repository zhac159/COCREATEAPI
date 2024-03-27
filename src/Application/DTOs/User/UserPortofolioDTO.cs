using Application.DTOs.PortofolioContentDTOs;

namespace Application.DTOs.UserDtos;

public class UserPortofolioDTO
{
    public string? AboutYou { get; set; }
    public List<PortofolioContentDTO>? PortofolioContents { get; set; }
}
