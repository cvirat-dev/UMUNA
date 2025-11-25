using Umuna.Server.Domain.Entities;

namespace Umuna.Server.Infrastructure.Repositories
{
    public interface IUserSettingsRepository : ICrudRepository<UserSettings>
    {
        Task<UserSettings?> GetByUserIdAsync(int userId);
    }
}
