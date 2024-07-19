using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class VoucherCodeRepository : IVoucherCodeRepository
{
    private readonly CoCreateDbContext context;

    public VoucherCodeRepository(CoCreateDbContext context)
    {
        this.context = context;
    }

    public async Task<VoucherCode?> GetVoucherCodeAsync(string code)
    {
        return await context.VoucherCode.AsSplitQuery().FirstOrDefaultAsync(vc => vc.Code == code);
    }

    public async Task<bool> DeleteAsync(VoucherCode voucherCode)
    {
        context.VoucherCode.Remove(voucherCode);
        await context.SaveChangesAsync();

        return true;
    }
}
