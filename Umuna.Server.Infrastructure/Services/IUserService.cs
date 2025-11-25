using Umuna.Core.Contracts.DTOs.User;
using Umuna.Core.Contracts.Models;

namespace Umuna.Server.Infrastructure.Services
{
    public interface IUserService
    {
        Task<ServiceResult<UserCreateDto>> AddUserAsync(UserCreateDto userDto);
        Task<ServiceResult<UserCreateDto>> GetUserByIdAsync(string id);
    }
}
