using Application.DTOs.EmailDTOs;
using Application.DTOs.ProjectDTOs;
using Application.DTOs.SkillDTOs;
using Application.DTOs.UserDtos;
using Application.Extensions;
using Application.Interfaces;
using Domain.Exceptions;
using Domain.Interfaces;
using Domain.Queries;
using NetTopologySuite.Geometries;

namespace Application.Services;

public class UserService : IUserService
{
    private readonly IUserRepository userRepository;
    private readonly IProjectRoleRepository projectRoleRepository;
    private readonly IStorageService storageService;
    private readonly ICurrentUserContextService currentUserContextService;
    private readonly IEmailService emailService;
    private readonly IMessageStorageService messageStorageService;

    public UserService(
        IUserRepository userRepository,
        IStorageService storageService,
        IProjectRoleRepository projectRoleRepository,
        ICurrentUserContextService currentUserContextService,
        IEmailService emailService,
        IMessageStorageService messageStorageService
    )
    {
        this.userRepository = userRepository;
        this.projectRoleRepository = projectRoleRepository;
        this.storageService = storageService;
        this.currentUserContextService = currentUserContextService;
        this.emailService = emailService;
        this.messageStorageService = messageStorageService;
    }

    public async Task<UserDTO> AuthenticateAsync(UserLoginDTO userLoginDTO)
    {
        var user = await userRepository.GetByUsernameOrEmailAsync(userLoginDTO.UsernameOrEmail);

        if (user is null)
        {
            throw new UsernameNotFoundException();
        }

        if (user.Password != userLoginDTO.Password)
        {
            throw new InvalidPasswordException();
        }

        return user.ToDTO();
    }

    public async Task<UserDTO> CreateAsync(UserCreateDTO userCreateDTO)
    {
        var user = userCreateDTO.ToEntity();

        if (await userRepository.GetByUsernameOrEmailAsync(user.Username) is not null)
        {
            throw new EntityAlreadyExistsException();
        }

        Random random = new Random();
        user.Coins = random.Next(10, 101);
        
        var createdUser = await userRepository.CreateAsync(user);

        await emailService.SendAndCacheEmailVerificationAsync(user.Email, createdUser.UserId);

        return createdUser.ToDTO();
    }

    public async Task<UserDTO> GetByIdAsync(int id)
    {
        var user = await userRepository.GetByIdIncludeAllPropertiesAsync(id);

        if (user is null)
        {
            throw new EntityNotFoundException();
        }

        return user.ToDTO();
    }

    public async Task<UserDTO> UpdateAsync(UserUpdateDTO userUpdateDTO, int userId)
    {
        var user = await userRepository.GetByIdIncludeAllPropertiesAsync(userId);

        if (user is null)
        {
            throw new EntityNotFoundException();
        }

        user.UpdateFromDTO(userUpdateDTO);

        var updatedUser = await userRepository.UpdateAsync(user);

        return updatedUser.ToDTO();
    }

    public async Task<List<SkillDTO>> UpdateSkillsAsync(
        List<SkillUpdateDTO> skillUpdateDTOs,
        int userId
    )
    {
        var user = await userRepository.GetByIdIncludeSkillsAsync(userId);

        if (user is null)
        {
            throw new EntityNotFoundException();
        }

        user.UpdateSkillsFromDTO(skillUpdateDTOs);

        var updatedUser = await userRepository.UpdateAsync(user);

        if (updatedUser.Skills is null)
        {
            return new List<SkillDTO>();
        }

        return updatedUser.Skills.Select(s => s.ToDTO()).ToList();
    }

    public async Task<UserLocationDTO> UpdateLocationAsync(
        UserLocationUpdateDTO userLocationUpdateDTO,
        int userId
    )
    {
        var user = await userRepository.GetByIdIncludeAllPropertiesAsync(userId);

        if (user is null)
        {
            throw new EntityNotFoundException();
        }

        user.Location = new Point(userLocationUpdateDTO.Longitude, userLocationUpdateDTO.Latitude)
        {
            SRID = 4326
        };
        user.Address = userLocationUpdateDTO.Address;

        var updatedUser = await userRepository.UpdateAsync(user);

        return updatedUser.ToLocationDTO();
    }

    public async Task<ProjectWithMatchingRolesListDTO> GetMatchingProjectRolesAsync(
        UserGetMatchingProjectRolesDTO userGetMatchingProjectRolesDTO,
        int userId
    )
    {
        var user = await userRepository.GetByIdIncludeSkillsAsync(userId);

        if (user is null)
        {
            throw new EntityNotFoundException();
        }

        var userSkillTypes = user.Skills.Select(s => s.SkillType).ToList();

        if (user.Location is null)
        {
            throw new LocationNotSetException();
        }

        var getMatchingProjectRolesAsyncQuery = new GetMatchingProjectRolesAsyncQuery
        {
            Location = user.Location,
            Distance = userGetMatchingProjectRolesDTO.Distance,
            SkillTypes = userSkillTypes,
            Effort = userGetMatchingProjectRolesDTO.Effort,
            UserId = userId,
        };

        var matchingProjects = await projectRoleRepository.GetMatchingProjectRoleIdsAsync(
            getMatchingProjectRolesAsyncQuery
        );
        var matchingProjectDTOs = matchingProjects
            .Select(mp => new ProjectWithMatchingRoleDTO
            {
                ProjectRoleId = mp.Item1,
                Project = mp.Item2.ToDTO(),
            })
            .ToList();

        var result = new ProjectWithMatchingRolesListDTO
        {
            ProjectWithMatchingRoles = matchingProjectDTOs
        };

        return result;
    }

    public async Task<UserPortofolioDTO> UpdatePortofolio(
        UserPortofolioUpdateDTO userPortofolioUpdateDTO
    )
    {
        var user = await userRepository.GetByIdIncludePortofolioAsync(
            currentUserContextService.GetUserId()
        );

        if (user is null)
        {
            throw new EntityNotFoundException();
        }

        await user.UpdatePortofolioFromDTOAsync(userPortofolioUpdateDTO, storageService);

        var updatedUser = await userRepository.UpdateAsync(user);

        return updatedUser.ToPortofolioDTO();
    }

    public async Task<bool> UpdatePublicKeyAsync(UserPublicKeyUpdateDTO userPublicKeyUpdateDTO)
    {
        var user = await userRepository.GetByIdIncludeAllPropertiesAsync(
            currentUserContextService.GetUserId()
        );

        if (user is null)
        {
            throw new EntityNotFoundException();
        }

        user.PublicKey = userPublicKeyUpdateDTO.PublicKey;

        var updatedUser = await userRepository.UpdateAsync(user);

        return updatedUser.PublicKey == userPublicKeyUpdateDTO.PublicKey;
    }

    public async Task<UserProfilesDTO> GetUserProfilesAsync(UserGetProfilesDTO userGetProfilesDTO)
    {
        var users = await userRepository.GetUsersProfileAsync(userGetProfilesDTO.UserIds);

        var userProfiles = users.Select(u => u.ToUserProfileDTO()).ToList();

        return new UserProfilesDTO { UserProfiles = userProfiles };
    }

    public async Task<bool> AdjustUserCoinsAsync(int coins)
    {
        var user = await userRepository.GetByIdAsync(currentUserContextService.GetUserId());

        if (user is null)
        {
            throw new EntityNotFoundException();
        }

        user.Coins += coins;

        if (user.Coins < 0)
        {
            throw new InsufficientFundsException();
        }

        await userRepository.UpdateAsync(user);

        return true;
    }

    public async Task<UserProfileDTO> GetUserProfileAsync(int userId)
    {
        var user = await userRepository.GetByIdIncludeAllPropertiesAsync(userId);

        if (user is null)
        {
            throw new EntityNotFoundException();
        }

        return user.ToUserProfileDTO();
    }

    public async Task<bool> VerifyEmailAsync(UserVerifyEmailDTO userVerifyEmailDTO)
    {
        var userId = currentUserContextService.GetUserId();

        var token = await messageStorageService.GetOneTimeEmailTokenAsync(
            userVerifyEmailDTO.Token,
            userId
        );

        if (token is null || token != userVerifyEmailDTO.Token)
        {
            throw new InvalidTokenException();
        }
        var user = await userRepository.GetByIdAsync(userId);

        if (user is null)
        {
            throw new EntityNotFoundException();
        }

        user.IsEmailVerified = true;

        await userRepository.UpdateAsync(user);

        return true;
    }

    public async Task ResendVerificationEmailAsync()
    {
        var user =
            await userRepository.GetByIdAsync(currentUserContextService.GetUserId())
            ?? throw new EntityNotFoundException();

        await emailService.SendAndCacheEmailVerificationAsync(
            user.Email,
            currentUserContextService.GetUserId()
        );
    }

    public async Task UpdateAndVerifyEmailAsync(UserUpdateEmailDTO userUpdateEmailDTO)
    {
        var user =
            await userRepository.GetByIdAsync(currentUserContextService.GetUserId())
            ?? throw new EntityNotFoundException();

        user.Email = userUpdateEmailDTO.Email;

        await userRepository.UpdateAsync(user);

        await emailService.SendAndCacheEmailVerificationAsync(
            user.Email,
            currentUserContextService.GetUserId()
        );
    }

    public async Task<bool> ChangePasswordAsync(UserChangePasswordDTO userChangePasswordDTO)
    {
        var user = await userRepository.GetByIdAsync(currentUserContextService.GetUserId());

        if (user is null)
        {
            throw new EntityNotFoundException();
        }

        if (user.Password != userChangePasswordDTO.OldPassword)
        {
            throw new InvalidPasswordException();
        }

        if (user.Password == userChangePasswordDTO.NewPassword)
        {
            throw new SamePasswordException();
        }

        user.Password = userChangePasswordDTO.NewPassword;

        await userRepository.UpdateAsync(user);

        return true;
    }
}
