namespace Application.Interfaces;

public interface ICurrentUserContextService
{
    int GetUserId();

    string GetEmail();
}