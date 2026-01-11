using Application.DTOs.UserDtos;

namespace Application.Interfaces;

public interface IAuthenticationService
{
    string CreateJWTTokenAsync(UserLoginResponseDTO user);
    int AuthenticateJWTTokenAndGetUserId(string token);
}