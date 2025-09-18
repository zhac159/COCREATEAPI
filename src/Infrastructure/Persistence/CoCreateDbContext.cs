using Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence
{
    public class CoCreateDbContext(DbContextOptions<CoCreateDbContext> options) : DbContext(options)
    {
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(CoCreateDbContext).Assembly);
        }
    }
}
