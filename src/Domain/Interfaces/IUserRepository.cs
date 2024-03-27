using Domain.Entities;

namespace Domain.Interfaces;

public interface IUserRepository
{
    Task<User?> GetByUsernameAsync(string name);
    Task<User> CreateAsync(User user);
    Task<bool> ExistsByNameAsync(string name);
    Task<User?> GetByIdAsync(int id);
    Task<User?> GetByIdIncludeAllPropertiesAsync(int id);
    Task<User?> GetByIdIncludeSkillsAsync(int id);
    Task<User?> GetByIdIncludePortofolioAsync(int id);
    Task<int?> GetCoinByIdAsync(int id);
    Task<int?> UpdateCoinByIdAsync(int id, int coin);
    Task<User> UpdateAsync(User user);
}
