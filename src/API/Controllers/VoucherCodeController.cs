using API.Factories;
using API.Models;
using Application.DTOs.VoucherCodeDTOs;
using Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

public class VoucherCodeController : COCREATEAPIControllerBase
{
    private readonly IVoucherCodeService voucherCodeService;

    public VoucherCodeController(IVoucherCodeService voucherCodeService)
    {
        this.voucherCodeService = voucherCodeService;
    }

    [HttpPost("redeem")]
    public async Task<ActionResult<APIResponse<RedeemVoucherCodeResponseDTO>>> Redeem(
        RedeemVoucherCodeDTO redeemVoucherCodeDTO
    )
    {
        var redeemVoucherCodeResponseDTO = await voucherCodeService.RedeemVoucherCode(
            redeemVoucherCodeDTO
        );

        return Ok(APIResponseFactory.CreateSuccess(redeemVoucherCodeResponseDTO));
    }
}
