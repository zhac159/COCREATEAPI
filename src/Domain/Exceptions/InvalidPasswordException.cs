namespace Domain.Exceptions;

public class InvalidPasswordException : Exception
{
    private const string message = "INVALID_PASSWORD";
    public InvalidPasswordException()
        : base(message) { }
}
