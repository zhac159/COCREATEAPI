using System.ComponentModel.DataAnnotations;

namespace Application.DTOs.VoucherCodeDTOs;

public class RedeemVoucherCodeDTO
{
    [Required]
    public required string Code { get; set; }
}