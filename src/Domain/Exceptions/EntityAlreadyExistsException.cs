namespace Domain.Exceptions;

public class EntityAlreadyExistsException : Exception
{
    private const string message = "entity-already-exists";
    public EntityAlreadyExistsException()
        : base(message) { }
}
