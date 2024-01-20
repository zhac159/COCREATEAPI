using Application.DTOs.UserDtos;
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

    public static UserDTO ToDTO(this User user)
    {
        return new UserDTO
        {
            UserId = user.UserId,
            Username = user.Username,
            Password = user.Password,
            Email = user.Email,
            Location = user.Location,
            AboutYou = user.AboutYou,
            Rating = user.Rating,
            TotalReviews = user.TotalReviews,
            ProfilePictureSrc = user.ProfilePictureSrc,
            BannerPictureSrc = user.BannerPictureSrc,
            Skills = user.Skills != null ? user.Skills.Select(s => s.ToDTO()).ToList() : null,
            PortofolioContents = user.PortofolioContents,
            ReviewsGiven = user.ReviewsGiven,
            ReviewsReceived = user.ReviewsReceived,
            Assets = user.Assets
        };
    }
}
