namespace Domain.Exceptions;

public class RoleAlreadyFilledException : Exception
{
    private const string message = "role-already-filled";
    public RoleAlreadyFilledException()
        : base(message) { }
}
