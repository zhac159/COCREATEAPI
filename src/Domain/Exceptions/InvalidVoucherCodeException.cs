namespace Domain.Exceptions;

public class InvalidVoucherCodeException : Exception
{
    private const string message = "INVALID_VOUCHER_CODE";
    public InvalidVoucherCodeException()
        : base(message) { }
}
