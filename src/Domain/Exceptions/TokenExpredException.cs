namespace Domain.Exceptions;

public class TokenExpiredException : Exception
{
    private const string message = "TOKEN_EXPIRED";
    public TokenExpiredException()
        : base(message) { }
}
