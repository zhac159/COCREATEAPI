namespace Application.DTOs.UserDtos;

public class UserLocationUpdateDTO
{
    public required double Longitude { get; set; }
    public required double Latitude { get; set; }
    public required string Address { get; set; }
}