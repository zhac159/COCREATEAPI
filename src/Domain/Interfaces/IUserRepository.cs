using Domain.Entities;

namespace Domain.Interfaces;

public interface IUserRepository
{
    Task<User?> GetByUsernameAsync(string name);
    Task<User> CreateAsync(User user);
    Task<bool> ExistsByNameAsync(string name);
    Task<User?> GetByIdAsync(int id);
    Task<User?> GetByIdIncludeOnlySkillsAsync(int id);
    Task<User> UpdateAsync(User user);
}
