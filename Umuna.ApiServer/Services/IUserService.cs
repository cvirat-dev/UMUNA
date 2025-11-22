using Umuna.ApiServer.DTOs;
using Umuna.Core.Models;

namespace Umuna.ApiServer.Services
{
    public interface IUserService
    {
        Task<ServiceResult<UserDto>> AddUserAsync(UserDto dto);
        Task<ServiceResult<UserDto>> GetUserByIdAsync(string id);
    }
}
