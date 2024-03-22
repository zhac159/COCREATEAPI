using Application.DTOs.SkillDTOs;
using Application.DTOs.UserDtos;
using Application.Interfaces;
using Domain.Entities;

namespace Application.Extensions;

public static class UserExtensions
{
    public static void UpdateFromDTO(this User user, UserUpdateDTO userUpdateDTO)
    {
        user.Username = userUpdateDTO.Username;
        user.Email = userUpdateDTO.Email;
        user.AboutYou = userUpdateDTO.AboutYou;

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
            Skills =
                user.Skills != null
                    ? user.Skills.Select(s => s.ToDTO()).ToList()
                    : new List<SkillDTO>(),
            PortofolioContents =
                user.PortofolioContents != null
                    ? user.PortofolioContents.Select(pc => pc.ToDTO()).ToList()
                    : null,
            // ReviewsGiven = user.ReviewsGiven,
            // ReviewsReceived = user.ReviewsReceived,
            Assets = user.Assets != null ? user.Assets.Select(a => a.ToDTO()).ToList() : null,
            Projects = user.Projects != null ? user.Projects.Select(p => p.ToDTO()).ToList() : null,
            Enquiries = user.Enquiries != null ? user.Enquiries.Select(e => e.ToDTO()).ToList() : null
        };
    }

    public static UserInformationDTO ToInformationDTO(this User user)
    {
        return new UserInformationDTO { UserId = user.UserId, Username = user.Username, };
    }

    public static UserLocationDTO ToLocationDTO(this User user)
    {
        return new UserLocationDTO { Address = user.Address };
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
            user.PortofolioContents?.RemoveAll(
                pc => !userPortofolioUpdateDTO.PortofolioContents.Any(pcu => pcu.Id == pc.Id)
            );

            var portofolioContentTasks = userPortofolioUpdateDTO.PortofolioContents.Select(
                async (portofolioContentUpdateDTO, order) =>
                {
                    var portofolioContent = user.PortofolioContents?.FirstOrDefault(
                        pc => pc.Id == portofolioContentUpdateDTO.Id
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
}
