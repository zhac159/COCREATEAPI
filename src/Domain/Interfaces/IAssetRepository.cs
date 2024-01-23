using Domain.Entities;

namespace Domain.Interfaces;

public interface IAssetRepository
{
    Task<Asset?> GetByIdAsync(int id);
    Task<Asset> CreateAsync(Asset asset);
    Task<Asset> UpdateAsync(Asset asset);
    Task<bool> DeleteAsync(Asset asset);
}
