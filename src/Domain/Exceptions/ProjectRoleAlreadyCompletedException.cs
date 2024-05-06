namespace Domain.Exceptions;

public class ProjectRoleAlreadyCompletedException : Exception
{
    private const string message = "project-role-already-completed";
    public ProjectRoleAlreadyCompletedException()
        : base(message) { }
}
