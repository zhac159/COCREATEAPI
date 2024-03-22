using Domain.Entities;

namespace Application.DTOs.EnquiryDTOs;

public class EnquiryCreateDTO
{
    public required int ProjectRoleId { get; set; }
    public required NewEnquiryMessageCreateDTO Message { get; set; }

    public Enquiry ToEntity(int enquirerId, int projectManagerId)
    {
        return new Enquiry { EnquirerId = enquirerId, ProjectRoleId = ProjectRoleId,  ProjectManagerId = projectManagerId,
            Messages = new List<EnquiryMessage> { Message.ToEntity(enquirerId) }
         };
    }
}
