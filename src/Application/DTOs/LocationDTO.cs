using System.ComponentModel.DataAnnotations;

namespace Application.DTOs;

public class LocationDTO
{
    
    [Required]
    public required double Longitude { get; set; }

    [Required]
    public required double Latitude { get; set; }

    [Required]
    public required string Address { get; set; }
}