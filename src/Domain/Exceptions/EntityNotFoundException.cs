namespace Domain.Exceptions;

public class EntityNotFoundException : Exception
{
    private const string message = "entity-not-found";
    public EntityNotFoundException()
        : base(message) { }
}
