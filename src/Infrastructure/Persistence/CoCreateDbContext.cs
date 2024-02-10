
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence
{
    public class CoCreateDbContext : DbContext
    {
        public CoCreateDbContext(DbContextOptions<CoCreateDbContext> options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Skill> Skills { get; set; }
        public DbSet<PortofolioContent> PortofolioContents { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<Asset> Assets { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<ProjectRole> ProjectRoles { get; set; }
        public DbSet<SeenMatches> SeenMatches { get; set; }
        public DbSet<Enquiry> Enquiries { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(CoCreateDbContext).Assembly);
        }
    }
}

