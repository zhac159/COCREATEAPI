namespace Domain.Exceptions;

public class InvalidPasswordException : Exception
{
    private const string message = "invalid-password";
    public InvalidPasswordException()
        : base(message) { }
}
