using Umuna.Core.Contracts.DTOs.UserSettings;
using Umuna.Core.Contracts.Models;

namespace Umuna.Server.Infrastructure.Services
{
    public interface IUserSettingsService : ICrudService<
        SettingsCreateDto,
        SettingsUpdateDto,
        SettingsReadDto>
    {
        Task<ServiceResult<SettingsReadDto>> GetByUserId(int userId);
    }
}
