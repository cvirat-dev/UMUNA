using Microsoft.EntityFrameworkCore;
using Umuna.Server.Domain.Entities;
using Umuna.Server.Infrastructure.Database;

namespace Umuna.Server.Infrastructure.Repositories
{
    public class UserSettingsRepository(UmunaDbContext context) : IUserSettingsRepository
    {
        private readonly UmunaDbContext _context = context;

        public async Task<UserSettings?> GetById(int id)
        {
            return await _context.Settings
                .Include(s => s.User)
                .FirstOrDefaultAsync(s => s.Id == id);
        }

        public async Task<List<UserSettings>> GetAll()
        {
            return await _context.Settings
                .Include(s => s.User)
                .ToListAsync();
        }

        public async Task Add(UserSettings entity)
        {
            await _context.Settings.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task Update(UserSettings entity)
        {
            entity.UpdatedAt = DateTime.UtcNow;
            _context.Settings.Update(entity);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            var settings = await _context.Settings.FindAsync(id);
            if (settings != null)
            {
                _context.Settings.Remove(settings);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<UserSettings?> GetByUserIdAsync(int userId)
        {
            return await _context.Settings
                .Include(s => s.User)
                .FirstOrDefaultAsync(s => s.UserId == userId);
        }
    }
}
