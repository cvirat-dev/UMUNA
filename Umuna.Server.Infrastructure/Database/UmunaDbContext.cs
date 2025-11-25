using Microsoft.EntityFrameworkCore;
using Umuna.Server.Domain.Entities;
using Umuna.Server.Infrastructure.Database.Configurations;

namespace Umuna.Server.Infrastructure.Database
{
    public class UmunaDbContext(DbContextOptions<UmunaDbContext> options) : DbContext(options)
    {
        public DbSet<User> Users => Set<User>();
        public DbSet<CameraPosition> CameraPositions => Set<CameraPosition>();
        public DbSet<UserSettings> Settings => Set<UserSettings>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new UserConfiguration());
            modelBuilder.ApplyConfiguration(new CameraPositionConfiguration());
            modelBuilder.ApplyConfiguration(new UserSettingsConfiguration());

            // Configure relationships, constraints, etc.
            base.OnModelCreating(modelBuilder);
        }
    }
}
