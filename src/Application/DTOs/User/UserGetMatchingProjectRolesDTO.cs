using Application.DTOs.AssetDTOs;
using Application.DTOs.PortofolioContentDTOs;
using Application.DTOs.SkillDTOs;
using Domain.Entities;

namespace Application.DTOs.UserDtos;

public class UserGetMatchingProjectRolesDTO
{
    public double Distance { get; set; }
    public int Effort { get; set; }
}
