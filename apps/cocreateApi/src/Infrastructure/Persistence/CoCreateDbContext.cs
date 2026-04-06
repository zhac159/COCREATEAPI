using Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence
{
    public class CoCreateDbContext(DbContextOptions<CoCreateDbContext> options) : DbContext(options)
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Skill> Skills { get; set; }
        public DbSet<PortfolioContentMedia> PortfolioContentMedias { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<ProjectMedia> ProjectMedias { get; set; }
        public DbSet<ProjectRole> ProjectRoles { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(CoCreateDbContext).Assembly);
        }
    }
}
