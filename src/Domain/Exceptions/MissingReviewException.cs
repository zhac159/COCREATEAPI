namespace Domain.Exceptions;

public class MissingReviewException : Exception
{
    private const string message = "missing-review";
    public MissingReviewException()
        : base(message) { }
}
