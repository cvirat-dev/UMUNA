using Microsoft.EntityFrameworkCore;
using Umuna.Core.Contracts.Models;
using Umuna.Server.Domain.Entities;
using Umuna.Server.Infrastructure.Database;

namespace Umuna.Server.Infrastructure.Repositories
{
    public class CameraPositionRepository(UmunaDbContext context) : ICameraPositionRepository
    {
        private readonly UmunaDbContext _context = context;

        public async Task<ServiceResult> AddAsync(CameraPosition cameraPosition)
        {
            try
            {
                _context.CameraPositions.Add(cameraPosition);
                await _context.SaveChangesAsync();
                return ServiceResult.Ok();
            }
            catch (Exception ex)
            {
                return ServiceResult.Fail(ex.Message);
            }
        }

        public async Task<ServiceResult> DeleteAsync(CameraPosition cameraPosition)
        {
            try
            {
                _context.CameraPositions.Remove(cameraPosition);
                await _context.SaveChangesAsync();
                return ServiceResult.Ok();
            }
            catch (Exception ex)
            {
                return ServiceResult.Fail(ex.Message);
            }
        }

        public async Task<ServiceResult<List<CameraPosition>>> GetAllAsync()
        {
            try
            {
                var cameraPositions = await _context.CameraPositions.ToListAsync();
                return ServiceResult<List<CameraPosition>>.Ok(cameraPositions);
            }
            catch (Exception ex)
            {
                return ServiceResult<List<CameraPosition>>.Fail(ex.Message);
            }
        }

        public async Task<ServiceResult<CameraPosition>> GetFirstByIdAsync(int id)
        {
            try
            {
                var cameraPosition = await _context.CameraPositions
                                                    .FirstOrDefaultAsync(cp => cp.Id == id);
                if (cameraPosition != null)
                {
                    return ServiceResult<CameraPosition>.Ok(cameraPosition);
                }
                else
                {
                    return ServiceResult<CameraPosition>.Fail("Camera position not found");
                }
            }
            catch (Exception ex)
            {
                return ServiceResult<CameraPosition>.Fail(ex.Message);
            }
        }

        public async Task<ServiceResult<List<CameraPosition>>> GetByUserIdAsync(int userId)
        {
            try
            {
                var cameraPositions = await _context.CameraPositions
                                                     .Where(cp => cp.UserId == userId)
                                                     .ToListAsync();
                return ServiceResult<List<CameraPosition>>.Ok(cameraPositions);
            }
            catch (Exception ex)
            {
                return ServiceResult<List<CameraPosition>>.Fail(ex.Message);
            }
        }

        public async Task<ServiceResult<CameraPosition>> GetDefaultForUserAsync(int userId)
        {
            try
            {
                var cameraPosition = await _context.CameraPositions
                                                    .Where(cp => cp.UserId == userId && cp.IsDefault)
                                                    .FirstOrDefaultAsync();
                if (cameraPosition != null)
                {
                    return ServiceResult<CameraPosition>.Ok(cameraPosition);
                }
                else
                {
                    return ServiceResult<CameraPosition>.Fail("Default camera position not found");
                }
            }
            catch (Exception ex)
            {
                return ServiceResult<CameraPosition>.Fail(ex.Message);
            }
        }

        public async Task<ServiceResult> UpdateAsync(CameraPosition cameraPosition)
        {
            try
            {
                _context.CameraPositions.Update(cameraPosition);
                await _context.SaveChangesAsync();
                return ServiceResult.Ok();
            }
            catch (Exception ex)
            {
                return ServiceResult.Fail(ex.Message);
            }
        }
    }
}
