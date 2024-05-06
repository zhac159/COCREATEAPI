namespace Domain.Exceptions;

public class InvalidRatingException : Exception
{
    private const string message = "invalid-rating";
    public InvalidRatingException()
        : base(message) { }
}
