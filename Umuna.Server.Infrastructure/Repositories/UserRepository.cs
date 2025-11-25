using Microsoft.EntityFrameworkCore;
using Umuna.Server.Domain.Entities;
using Umuna.Server.Infrastructure.Database;

namespace Umuna.Server.Infrastructure.Repositories
{
    public class UserRepository(UmunaDbContext context) : IUserRepository
    {
        private readonly UmunaDbContext _context = context;

        public async Task<User?> GetById(int id)
        {
            return await _context.Users
                .Include(u => u.Settings)
                .Include(u => u.CameraPositions)
                .FirstOrDefaultAsync(u => u.Id == id);
        }

        public async Task<List<User>> GetAll()
        {
            return await _context.Users
                .Include(u => u.Settings)
                .Include(u => u.CameraPositions)
                .ToListAsync();
        }

        public async Task Add(User entity)
        {
            await _context.Users.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task Update(User entity)
        {
            _context.Users.Update(entity);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user != null)
            {
                _context.Users.Remove(user);
                await _context.SaveChangesAsync();
            }
        }
    }
}
