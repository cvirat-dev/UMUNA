using Microsoft.EntityFrameworkCore;
using Umuna.Core.Domain.Data;

namespace Umuna.ApiServer.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<User> Users => Set<User>();

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configure relationships, constraints, etc.
            base.OnModelCreating(modelBuilder);
        }
    }
}
