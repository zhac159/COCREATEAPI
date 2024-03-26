using Application.DTOs.UserDtos;

namespace Application.DTOs.EnquiryDTOs;

public class EnquiryDTO
{
    public required int Id { get; set; }
    public required int ProjectRoleId { get; set; }
    public UserInformationDTO? Enquirer { get; set; }
    public UserInformationDTO? ProjectManager { get; set; }
}
