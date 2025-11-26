using Umuna.Core.Contracts.DTOs.Requests;
using Umuna.Core.Contracts.DTOs.User;
using Umuna.Core.Contracts.Models;

namespace Umuna.Server.Infrastructure.Services
{
    public interface IAuthService
    {
        Task<ServiceResult<UserReadDto>> Authenticate(LoginRequestDto loginRequestDto);
    }
}