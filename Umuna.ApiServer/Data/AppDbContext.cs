using Microsoft.EntityFrameworkCore;
using Umuna.Core.Domain.Data;

namespace Umuna.ApiServer.Data
{
    public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
    {
        public DbSet<User> Users => Set<User>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configure relationships, constraints, etc.
            base.OnModelCreating(modelBuilder);
        }
    }
}
