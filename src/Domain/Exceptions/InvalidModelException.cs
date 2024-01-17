namespace Domain.Exceptions;

public class InvalidModelException : Exception
{
    private const string message = "invalid-model";
    public InvalidModelException()
        : base(message) { }
}
