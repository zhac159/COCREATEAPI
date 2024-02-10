using Domain.Entities;

namespace Application.DTOs.EnquiryDTOs;

public class EnquiryCreateDTO
{
    public required int ProjectRoleId { get; set; }

    public Enquiry ToEntity(int userId)
    {
        return new Enquiry { UserId = userId, ProjectRoleId = ProjectRoleId };
    }
}
