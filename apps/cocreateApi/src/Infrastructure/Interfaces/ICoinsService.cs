namespace Application.Interfaces;

public interface ICoinsService
{
    Task<bool> CanAfford(int cost);
    Task DeductCoins(int amount);
}
