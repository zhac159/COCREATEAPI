using Domain.Entities;

namespace Domain.Interfaces;

public interface IUserRepository
{
    Task<User?> GetByUsernameAsync(string name);
    Task<User> CreateAsync(User user);

    Task<bool> ExistsByNameAsync(string name);
}
