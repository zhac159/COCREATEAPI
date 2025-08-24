using Application.DTOs.ProjectDTOs;
using Application.DTOs.SkillDTOs;
using Application.DTOs.UserDtos;

namespace Application.Interfaces;

public interface IUserService
{
    Task<UserLoginResponseDTO> AuthenticateAsync(UserLoginDTO userLoginDTO);
    Task<UserLoginResponseDTO> CreateAsync(UserCreateDTO user);
    Task<UserDTO> GetByIdAsync(int id);
    Task<UserLoginResponseDTO> GetUserLoginResponseByIdAsync(int id);
    Task<UserDTO> UpdateAsync(UserUpdateDTO userUpdateDTO);
    Task<List<SkillDTO>> UpdateSkillsAsync(List<SkillUpdateDTO> userUpdateDTO, int userId);
    Task<UserLocationDTO> UpdateLocationAsync(UserLocationUpdateDTO location, int userId);
    Task<ProjectWithMatchingRolesListDTO> GetMatchingProjectRolesAsync(
        UserGetMatchingProjectRolesDTO userGetMatchingProjectRolesDTO,
        int userId
    );
    Task<bool> UpdatePublicKeyAsync(UserPublicKeyUpdateDTO userPublicKeyUpdateDTO);
    Task<UserProfilesDTO> GetUserProfilesAsync(UserGetProfilesDTO userGetProfilesDTO);
    Task<bool> AdjustUserCoinsAsync(int coins);
    Task<UserProfileDTO> GetUserProfileAsync(int userId);
    Task<UserProfileDetailsDTO> GetProfileDetails();
    Task<bool> VerifyEmailAsync(UserVerifyEmailDTO userVerifyEmailDTO);
    Task ResendVerificationEmailAsync();
    Task UpdateAndVerifyEmailAsync(UserUpdateEmailDTO userUpdateEmailDTO);
    Task<bool> ChangePasswordAsync(UserChangePasswordDTO userChangePasswordDTO);
    Task<bool> DeleteAsync();
    Task<UserLoginResponseDTO> GetByIdUserLoginInfoAsync(int id);
}
