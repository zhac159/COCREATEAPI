namespace Application.Interfaces;

public interface ICurrentUser
{
    int GetUserId();

    string GetEmail();
}
