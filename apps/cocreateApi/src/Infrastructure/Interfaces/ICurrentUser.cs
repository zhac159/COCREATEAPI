namespace Infrastructure.Interfaces;

public interface ICurrentUser
{
    int GetUserId();

    string GetEmail();
}
