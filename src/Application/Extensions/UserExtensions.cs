using Application.DTOs;
using Application.DTOs.Chat;
using Application.DTOs.MediaDTOs;
using Application.DTOs.SkillDTOs;
using Application.DTOs.UserDtos;
using Application.Interfaces;
using Domain.Entities;
using Domain.Enums;
using NetTopologySuite.Geometries;

namespace Application.Extensions;

public static class UserExtensions
{
    public static void UpdateFromDTO(this User user, UserUpdateDTO userUpdateDTO)
    {
        user.Username = userUpdateDTO.Username;
        user.Email = userUpdateDTO.Email;
        user.AboutYou = userUpdateDTO.AboutYou;
        user.Location = new Point(userUpdateDTO.Location.Longitude, userUpdateDTO.Location.Latitude)
        {
            SRID = 4326
        };
        user.Address = userUpdateDTO.Location.Address;

        if (userUpdateDTO.Skills is not null)
        {
            user.Skills?.RemoveAll(s => !userUpdateDTO.Skills.Any(su => su.Id == s.Id));

            user.Skills = userUpdateDTO
                .Skills.Select(su =>
                {
                    var skill = user.Skills?.FirstOrDefault(s => s.Id == su.Id);
                    if (skill is not null)
                    {
                        skill.UpdateFromDTO(su);
                        return skill;
                    }
                    return su.ToEntity();
                })
                .ToList();
        }
    }

    public static void UpdateSkillsFromDTO(this User user, List<SkillUpdateDTO> skillUpdateDTOs)
    {
        user.Skills?.RemoveAll(s => !skillUpdateDTOs.Any(su => su.Id == s.Id));

        user.Skills = skillUpdateDTOs
            .Select(su =>
            {
                var skill = user.Skills?.FirstOrDefault(s => s.Id == su.Id);
                if (skill is not null)
                {
                    skill.UpdateFromDTO(su);
                    return skill;
                }
                return su.ToEntity();
            })
            .ToList();
    }

    public static UserDTO ToDTO(this User user)
    {
        return new UserDTO
        {
            UserId = user.UserId,
            Username = user.Username,
            Email = user.Email,
            Address = user.Address,
            AboutYou = user.AboutYou,
            Coins = user.Coins,
            Rating = user.Rating,
            TotalReviews = user.TotalReviews,
            ProfilePictureSrc = user.ProfilePictureSrc,
            Longitude = user.Location != null ? user.Location.X : 0,
            Latitude = user.Location != null ? user.Location.Y : 0,
            BannerPictureSrc = user.BannerPictureSrc,
            PublicKey = user.PublicKey,
            Skills = user.Skills.Select(s => s.ToDTO()).ToList(),
            PortofolioContents = user.PortofolioContents.Select(pc => pc.ToDTO()).ToList(),
            ReviewsReceived = user.ReviewsReceived.Select(r => r.ToDTO()).ToList(),
            Experiences = user.Experiences.Select(e => e.ToDTO()).ToList(),
            Assets = user.Assets.Select(a => a.ToDTO()).ToList(),
            Projects = user.Projects.Select(p => p.ToDTO()).ToList(),
            Enquiries = user.Enquiries.Select(e => e.ToDTO()).ToList(),
            AssignedProjects = user.ProjectRoles.Select(pr => pr.Project!.ToDTO()).ToList(),
            AssetOffers = user
                .Assets.SelectMany(a => a.AssetOffers)
                .Select(ao => ao.ToDTO())
                .ToList()
        };
    }

    public static ChatMemberDTO ToChatMemberDTO(this User user)
    {
        return new ChatMemberDTO
        {
            UserId = user.UserId,
            UserName = user.Username,
            ProfilePicture = user.ProfilePictureSrc ?? "",
            PublicKey = user.PublicKey ?? ""
        };
    }

    public static UserLoginResponseDTO ToUserLoginResponseDTO(this User user)
    {
        return new UserLoginResponseDTO
        {
            UserId = user.UserId,
            Username = user.Username,
            Email = user.Email,
            PublicKey = user.PublicKey,
            BannerPictureSrc = user.BannerPictureSrc,
            Coins = user.Coins,
            Chats = user.ChatMemberships.Select(cm => cm.Chat.ToDTO()).ToList(),
            ProjectsManaging = user.Projects.Select(p => p.ToInfoDTO()).ToList(),
        };
    }

    public static UserInformationDTO ToInformationDTO(this User user)
    {
        return new UserInformationDTO
        {
            UserId = user.UserId,
            Username = user.Username,
            PublicKey = user.PublicKey,
            Rating = user.Rating
        };
    }

    public static UserLocationDTO ToUserLocationDTO(this User user)
    {
        return new UserLocationDTO { Address = user.Address };
    }

    public static LocationDTO? ToLocationDTO(this User user)
    {
        if (user.Location is null)
            return null;
        return new LocationDTO
        {
            Address = user.Address ?? "",
            Latitude = user.Location.Y,
            Longitude = user.Location.X
        };
    }

    public static async Task UpdatePortofolioFromDTOAsync(
        this User user,
        UserPortofolioUpdateDTO userPortofolioUpdateDTO,
        IStorageService storageService
    )
    {
        if (userPortofolioUpdateDTO.AboutYou is not null)
        {
            user.AboutYou = userPortofolioUpdateDTO.AboutYou;
        }

        if (userPortofolioUpdateDTO.PortofolioContents is not null)
        {
            user.PortofolioContents?.RemoveAll(pc =>
                !userPortofolioUpdateDTO.PortofolioContents.Any(pcu => pcu.Id == pc.Id)
            );

            var portofolioContentTasks = userPortofolioUpdateDTO.PortofolioContents.Select(
                async (portofolioContentUpdateDTO, order) =>
                {
                    var portofolioContent = user.PortofolioContents?.FirstOrDefault(pc =>
                        pc.Id == portofolioContentUpdateDTO.Id
                    );
                    if (portofolioContent is not null)
                    {
                        await portofolioContent.UpdateFromDTOAsync(
                            portofolioContentUpdateDTO,
                            storageService
                        );
                        return portofolioContent;
                    }
                    return portofolioContentUpdateDTO.ToPortofolioContentEntity();
                }
            );

            user.PortofolioContents = (await Task.WhenAll(portofolioContentTasks)).ToList();
        }
    }

    public static UserPortofolioDTO ToPortofolioDTO(this User user)
    {
        return new UserPortofolioDTO
        {
            AboutYou = user.AboutYou,
            PortofolioContents =
                user.PortofolioContents != null
                    ? user.PortofolioContents.Select(pc => pc.ToDTO()).ToList()
                    : null
        };
    }

    public static UserProfileDTO ToUserProfileDTO(this User user)
    {
        return new UserProfileDTO
        {
            UserId = user.UserId,
            Username = user.Username,
            AboutYou = user.AboutYou,
            Rating = user.Rating,
            TotalReviews = user.TotalReviews,
            Skills = user.Skills.Select(s => s.ToDTO()).ToList(),
            ReviewsReceived = user.ReviewsReceived.Select(r => r.ToDTO()).ToList(),
            PortofolioContents = user.PortofolioContents.Select(pc => pc.ToDTO()).ToList(),
            Experiences = user.Experiences.Select(e => e.ToDTO()).ToList()
        };
    }

    public static UserProfileDetailsDTO ToUserProfileDetailsDTO(this User user)
    {
        return new UserProfileDetailsDTO
        {
            Username = user.Username,
            Email = user.Email,
            AboutYou = user.AboutYou,
            Location = user.ToLocationDTO(),
            Skills = user.Skills.Select(s => s.ToDTO()).ToList(),
            ProfilePicture = new MediaDTO
            {
                Uri = user.ProfilePictureSrc ?? "",
                MediaType = MediaType.Image
            },
        };
    }
}
