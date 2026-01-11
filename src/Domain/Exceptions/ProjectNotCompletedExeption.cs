namespace Domain.Exceptions;

public class ProjectNotCompletedExeption : Exception
{
    private const string message = "project-not-completed";
    public ProjectNotCompletedExeption()
        : base(message) { }
}
