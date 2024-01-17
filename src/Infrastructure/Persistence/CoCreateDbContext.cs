
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence
{
    public class CoCreateDbContext : DbContext
    {
        public CoCreateDbContext(DbContextOptions<CoCreateDbContext> options) : base(options)
        {
        }

        public DbSet<TestTable> TestTables { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(CoCreateDbContext).Assembly);
        }
    }
}