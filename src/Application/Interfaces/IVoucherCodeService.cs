using Application.DTOs.VoucherCodeDTOs;

namespace Application.Interfaces;

public interface IVoucherCodeService
{
    Task<RedeemVoucherCodeResponseDTO> RedeemVoucherCode(RedeemVoucherCodeDTO redeemVoucherCodeDTO);
}
