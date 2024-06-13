using System.ComponentModel.DataAnnotations;
using Domain.Enums;

namespace Application.DTOs.MediaDTOs;

public class MediaDTO
{
    [Required]
    public int Id { get; set; }

    [Required]
    public required string Uri { get; set; }

    [Required]
    public required MediaType MediaType { get; set; }
}
