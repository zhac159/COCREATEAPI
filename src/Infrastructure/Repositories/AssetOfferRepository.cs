using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class AssetOfferRepository : IAssetOfferRepository
{
    private readonly CoCreateDbContext context;

    public AssetOfferRepository(CoCreateDbContext context)
    {
        this.context = context;
    }

    public async Task<AssetOffer?> CreateAsync(AssetOffer assetOffer)
    {
        await context.AssetOffers.AddAsync(assetOffer);
        await context.SaveChangesAsync();

        return await context
            .AssetOffers.Include(ao => ao.Asset)
            .ThenInclude(asset => asset!.User)
            .FirstOrDefaultAsync(ao => ao.Id == assetOffer.Id);
    }
}
