using System.ComponentModel.DataAnnotations;
using Application.DTOs.UserDtos;
using Domain.Enums;

namespace Application.DTOs.AssetDTOs;

public class AssetInformationDTO
{
    [Required]
    public int Id { get; set; }

    [Required]
    public required string Name { get; set; }

    [Required]
    public required string Description { get; set; }

    [Required]
    public required AssetType AssetType { get; set; }
    public UserInformationDTO? Owner { get; set; }
}
