using Microsoft.EntityFrameworkCore;
using Umuna.Core.Contracts.Models;
using Umuna.Server.Domain.Entities;
using Umuna.Server.Infrastructure.Database;

namespace Umuna.Server.Infrastructure.Repositories
{
    public class UserSettingsRepository : IUserSettingsRepository
    {
        private readonly UmunaDbContext _context;

        public UserSettingsRepository(UmunaDbContext context)
        {
            _context = context;
        }

        public async Task<ServiceResult> AddAsync(UserSettings settings)
        {
            try
            {
                _context.Settings.Add(settings);
                await _context.SaveChangesAsync();
                return ServiceResult.Ok();
            }
            catch (Exception ex)
            {
                return ServiceResult.Fail(ex.Message);
            }
        }

        public async Task<ServiceResult> DeleteAsync(UserSettings settings)
        {
            try
            {
                _context.Settings.Remove(settings);
                await _context.SaveChangesAsync();
                return ServiceResult.Ok();
            }
            catch (Exception ex)
            {
                return ServiceResult.Fail(ex.Message);
            }
        }

        public async Task<ServiceResult<UserSettings>> GetByUserIdAsync(int userId)
        {
            try
            {
                var settings = await _context.Settings
                                             .SingleOrDefaultAsync(s => s.UserId == userId);
                if (settings != null)
                {
                    return ServiceResult<UserSettings>.Ok(settings);
                }
                else
                {
                    return ServiceResult<UserSettings>.Fail("User settings not found");
                }
            }
            catch (Exception ex)
            {
                return ServiceResult<UserSettings>.Fail(ex.Message);
            }
        }

        public async Task<ServiceResult> UpdateAsync(UserSettings settings)
        {
            try
            {
                _context.Settings.Update(settings);
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
