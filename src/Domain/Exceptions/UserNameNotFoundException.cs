namespace Domain.Exceptions;

public class UsernameNotFoundException : Exception
{
    private const string message = "USERNAME_NOT_FOUND";
    public UsernameNotFoundException()
        : base(message) { }
}
