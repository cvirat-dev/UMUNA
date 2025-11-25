using Microsoft.EntityFrameworkCore;
using Umuna.Server.Domain.Entities;
using Umuna.Server.Infrastructure.Database;

namespace Umuna.Server.Infrastructure.Repositories
{
    public class CameraPositionRepository(UmunaDbContext context) : ICameraPositionRepository
    {
        private readonly UmunaDbContext _context = context;

        public async Task Add(CameraPosition entity)
        {
            if (entity == null) return;
            entity.CreatedAt = DateTime.UtcNow;
            _context.CameraPositions.Add(entity);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            CameraPosition? existing = await _context.CameraPositions.FirstOrDefaultAsync(cp => cp.Id == id);
            if (existing == null) return;
            _context.CameraPositions.Remove(existing);
            await _context.SaveChangesAsync();
        }

        public async Task<List<CameraPosition>> GetAll()
        {
            return await _context.CameraPositions.ToListAsync();
        }

        public async Task<CameraPosition?> GetById(int id)
        {
            return await _context.CameraPositions.FirstOrDefaultAsync(cp => cp.Id == id);
        }

        public async Task<List<CameraPosition>> GetByUserId(int userId)
        {
            return await _context.CameraPositions.Where(cp => cp.UserId == userId).ToListAsync();
        }

        public async Task<CameraPosition?> GetDefaultForUser(int userId)
        {
            return await _context.CameraPositions.FirstOrDefaultAsync(cp => cp.UserId == userId && cp.IsDefault);
        }

        public async Task Update(CameraPosition entity)
        {
            if (entity == null) return;
            entity.UpdatedAt = DateTime.UtcNow;
            _context.CameraPositions.Update(entity);
            await _context.SaveChangesAsync();
        }
    }
}
