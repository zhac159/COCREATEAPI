namespace Domain.Entities;

public class Enquiry
{
    public int Id { get; set; }
    public int EnquirerId { get; set; }
    public User? Enquirer { get; set; }
    public int ProjectManagerId { get; set; }
    public User? ProjectManager { get; set; }
    public int ProjectRoleId { get; set; }
    public ProjectRole? ProjectRole { get; set; }
    public DateTime CreateAt { get; set; }
    public List<EnquiryMessage> Messages { get; set; } = new List<EnquiryMessage>();
}
