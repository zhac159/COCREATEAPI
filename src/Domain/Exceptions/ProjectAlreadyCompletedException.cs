namespace Domain.Exceptions;

public class ProjectAlreadyCompletedException : Exception
{
    private const string message = "project-already-completed";
    public ProjectAlreadyCompletedException()
        : base(message) { }
}
