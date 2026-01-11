namespace Domain.Exceptions;

public class SamePasswordException : Exception
{
    private const string message = "SAME_PASSWORD";
    public SamePasswordException()
        : base(message) { }
}
