using System.ComponentModel.DataAnnotations;

namespace Application.DTOs.AssetDTOs;

public class AssetSearchResultDTO
{
    [Required]
    public List<AssetDTO> Assets { get; set; } = new List<AssetDTO>();
}
