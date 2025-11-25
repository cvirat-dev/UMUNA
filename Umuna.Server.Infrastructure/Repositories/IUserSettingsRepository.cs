using Umuna.Core.Contracts.Models;
using Umuna.Server.Domain.Entities;

namespace Umuna.Server.Infrastructure.Repositories
{
    public interface IUserSettingsRepository
    {
        Task<ServiceResult<UserSettings>> GetByUserIdAsync(int userId);
        Task<ServiceResult> AddAsync(UserSettings settings);
        Task<ServiceResult> UpdateAsync(UserSettings settings);
        Task<ServiceResult> DeleteAsync(UserSettings settings);
    }
}
