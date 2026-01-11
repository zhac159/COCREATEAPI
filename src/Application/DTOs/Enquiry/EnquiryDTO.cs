using System.ComponentModel.DataAnnotations;
using Application.DTOs.UserDtos;

namespace Application.DTOs.EnquiryDTOs;

public class EnquiryDTO
{
    [Required]
    public required int Id { get; set; }

    [Required]
    public required int ProjectRoleId { get; set; }
    public UserInformationDTO? Enquirer { get; set; }
    public UserInformationDTO? ProjectManager { get; set; }
    public int? ProjectId { get; set; }

    [Required]
    public required string EnquiryMessage { get; set; }

    [Required]
    public required bool Shortlisted { get; set; }
}
