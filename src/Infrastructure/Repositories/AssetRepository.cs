using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Persistence;

namespace Infrastructure.Repositories;

public class AssetRepository : IAssetRepository
{
    private readonly CoCreateDbContext context;

    public AssetRepository(CoCreateDbContext context)
    {
        this.context = context;
    }

    public async Task<Asset> CreateAsync(Asset asset)
    {
        await context.Assets.AddAsync(asset);
        await context.SaveChangesAsync();

        return asset;
    }

    // public async Task<Asset?> GetByIdAsync(int id)
    // {
    //     var asset = await context.Assets.Where(a => a.Id == id).FirstOrDefaultAsync();

    //     return asset;
    // }

    // public async Task<Asset?> GetByUserIdAsync(int userId)
    // {
    //     var asset = await context.Assets.Where(a => a.UserId == userId).FirstOrDefaultAsync();

    //     return asset;
    // }

    // public async Task<Asset> UpdateAsync(Asset asset)
    // {
    //     context.Assets.Update(asset);
    //     await context.SaveChangesAsync();

    //     return asset;
    // }
}