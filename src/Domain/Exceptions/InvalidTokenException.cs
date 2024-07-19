namespace Domain.Exceptions;

public class InvalidTokenException : Exception
{
    private const string message = "invalid-token";
    public InvalidTokenException()
        : base(message) { }
}
