using Application.DTOs.VoucherCodeDTOs;
using Application.Interfaces;
using Domain.Exceptions;
using Domain.Interfaces;

namespace Application.Services;

public class VoucherCodeService : IVoucherCodeService
{
    private readonly IVoucherCodeRepository voucherCodeRepository;
    private readonly IUserRepository userRepository;
    private readonly ICurrentUserContextService currentUserContextService;

    public VoucherCodeService(
        IVoucherCodeRepository voucherCodeRepository,
        IUserRepository userRepository,
        ICurrentUserContextService currentUserContextService
    )
    {
        this.voucherCodeRepository = voucherCodeRepository;
        this.userRepository = userRepository;
        this.currentUserContextService = currentUserContextService;
    }

    public async Task<RedeemVoucherCodeResponseDTO> RedeemVoucherCode(RedeemVoucherCodeDTO redeemVoucherCodeDTO)
    {
        var voucherCode = await voucherCodeRepository.GetVoucherCodeAsync(
            redeemVoucherCodeDTO.Code
        );

        if (voucherCode == null)
        {
            throw new InvalidVoucherCodeException();
        }

        var newCoins = await userRepository.AddCoinsByIdAsync(
            currentUserContextService.GetUserId(),
            voucherCode.Amount
        );
        
        await voucherCodeRepository.DeleteAsync(voucherCode);

        return new RedeemVoucherCodeResponseDTO
        {
            Coin = newCoins
        };
    }
}
