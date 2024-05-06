namespace Domain.Exceptions;

public class ProjectNotExistException : Exception
{
    private const string message = "project-not-exist";
    public ProjectNotExistException()
        : base(message) { }
}
