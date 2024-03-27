using Application.DTOs.AssetDTOs;

namespace Application.Interfaces;

public interface IAssetService
{
    Task<AssetDTO> CreateAsync(AssetCreateDTO assetCreateDTO);
    Task<AssetDTO> UpdateAsync(AssetUpdateDTO assetUpdateDTO);
    Task<bool> DeleteAsync(int id); 
}