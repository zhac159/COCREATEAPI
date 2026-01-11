namespace Domain.Exceptions;

public class InsufficientFundsException : Exception
{
    private const string message = "insufficient-funds";
    public InsufficientFundsException()
        : base(message) { }
}
