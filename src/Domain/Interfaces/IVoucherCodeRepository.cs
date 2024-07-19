using Domain.Entities;

namespace Domain.Interfaces;

public interface IVoucherCodeRepository
{
    Task<VoucherCode?> GetVoucherCodeAsync(string code);
    Task<bool> DeleteAsync(VoucherCode voucherCode);
}
