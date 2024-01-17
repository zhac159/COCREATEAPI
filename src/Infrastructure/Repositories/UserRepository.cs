using System.Linq;
using System.Threading.Tasks;
using Domain.Entities;
using Domain.Exceptions;
using Domain.Interfaces;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class UserRepository : IUserRepository
{
        private readonly CoCreateDbContext _context;

    public UserRepository(CoCreateDbContext context)
        {
            _context = context;
        }


    public async Task<User?> GetByUsernameAsync(string name)
    {
        var user = await _context.Users.Where(u => u.Username == name).FirstOrDefaultAsync();

        return user;
    }

    public async Task<User> CreateAsync(User user)
    {

        await _context.Users.AddAsync(user);
        await _context.SaveChangesAsync();

        return user;
    }

    public async Task<bool> ExistsByNameAsync(string name)
    {
        var user = await _context.Users.Where(u => u.Username == name).FirstOrDefaultAsync();

        return user != null;
    }
}
