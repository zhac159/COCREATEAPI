namespace Application.DTOs.UserDtos;

public class UserInformationDTO
{
    public int UserId { get; set; }
    public required string Username { get; set; }
    public string? PublicKey { get; set; }
}
