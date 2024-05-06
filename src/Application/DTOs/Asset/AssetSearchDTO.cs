using System.ComponentModel.DataAnnotations;
using Domain.Enums;

namespace Application.DTOs.AssetDTOs;

public class AssetSearchDTO
{
    public string SearchTerm { get; set; } = string.Empty;

    public AssetType? AssetType { get; set; }
}
