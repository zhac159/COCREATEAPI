namespace Domain.Exceptions;

public class ProjectNotCompletedException : Exception
{
    private const string message = "project-not-completed-exception";
    public ProjectNotCompletedException()
        : base(message) { }
}
