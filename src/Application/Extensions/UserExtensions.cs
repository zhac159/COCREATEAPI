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
        user.ProfilePictureSrc = userUpdateDTO.ProfilePicture?.Uri;

        user.PortfolioMedias =
        [
            .. userUpdateDTO.PortfolioMedias.Select(
                (pm, index) => pm.ToPortfolioContentMediaEntity(index)
            )
        ];

        if (userUpdateDTO.Skills is not null)
        {
            user.Skills?.RemoveAll(s => !userUpdateDTO.Skills.Any(su => su.Id == s.Id));

            user.Skills =
            [
                .. userUpdateDTO.Skills.Select(su =>
                {
                    var skill = user.Skills?.FirstOrDefault(s => s.Id == su.Id);
                    if (skill is not null)
                    {
                        skill.UpdateFromDTO(su);
                        return skill;
                    }
                    return su.ToEntity();
                })
            ];
        }
    }

    public static void UpdateSkillsFromDTO(this User user, List<SkillUpdateDTO> skillUpdateDTOs)
    {
        user.Skills?.RemoveAll(s => !skillUpdateDTOs.Any(su => su.Id == s.Id));

        user.Skills =
        [
            .. skillUpdateDTOs.Select(su =>
            {
                var skill = user.Skills?.FirstOrDefault(s => s.Id == su.Id);
                if (skill is not null)
                {
                    skill.UpdateFromDTO(su);
                    return skill;
                }
                return su.ToEntity();
            })
        ];
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
            Skills = [.. user.Skills.Select(s => s.ToDTO())],
            ReviewsReceived = [.. user.ReviewsReceived.Select(r => r.ToDTO())],
            Experiences = [.. user.Experiences.Select(e => e.ToDTO())],
            Assets = [.. user.Assets.Select(a => a.ToDTO())],
            Projects = [.. user.Projects.Select(p => p.ToDTO())],
            Enquiries = [.. user.Enquiries.Select(e => e.ToDTO())],
            AssignedProjects = [.. user.ProjectRoles.Select(pr => pr.Project!.ToDTO())],
            AssetOffers = [.. user.Assets.SelectMany(a => a.AssetOffers).Select(ao => ao.ToDTO())]
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
            ProfilePicture = user.ProfilePictureSrc,
            Coins = user.Coins,
            Chats = [.. user.ChatMemberships.Select(cm => cm.Chat.ToDTO())],
            ProjectsManaging = [.. user.Projects.Select(p => p.ToInfoDTO())],
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

    public static UserProfileDTO ToUserProfileDTO(this User user)
    {
        return new UserProfileDTO
        {
            UserId = user.UserId,
            Username = user.Username,
            AboutYou = user.AboutYou,
            Rating = user.Rating,
            TotalReviews = user.TotalReviews,
            Skills = [.. user.Skills.Select(s => s.ToDTO())],
            ReviewsReceived = [.. user.ReviewsReceived.Select(r => r.ToDTO())],
            Experiences = [.. user.Experiences.Select(e => e.ToDTO())]
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
            Skills = [.. user.Skills.Select(s => s.ToDTO())],
            ProfilePicture = new MediaDTO
            {
                Uri = user.ProfilePictureSrc ?? "",
                MediaType = MediaType.Image
            },
            PortfolioMedias = [.. user.PortfolioMedias.Select(pm => pm.ToDTO())]
        };
    }
}
