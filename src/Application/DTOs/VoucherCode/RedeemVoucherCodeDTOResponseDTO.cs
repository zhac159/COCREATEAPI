using System.ComponentModel.DataAnnotations;

namespace Application.DTOs.VoucherCodeDTOs;

public class RedeemVoucherCodeResponseDTO
{
    [Required]
    public required int Coin { get; set; }
}