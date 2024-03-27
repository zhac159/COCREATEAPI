using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

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

    public async Task<Asset?> GetByIdIncludeAllPropertiesAsync(int id)
    {
        var asset = await context.Assets
            .Include(asset => asset.Medias)
            .FirstOrDefaultAsync(asset => asset.Id == id);

        return asset;
    }
    public async Task<bool> DeleteAsync(Asset asset)
    {
        context.Assets.Remove(asset);
        await context.SaveChangesAsync();

        return true;
    }

    public async Task<Asset> UpdateAsync(Asset asset)
    {
        context.Assets.Update(asset);
        await context.SaveChangesAsync();

        return asset;
    }
}