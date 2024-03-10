namespace Application.Interfaces;
public interface IChatHubService
{
    Task SendMessage(string message);
}