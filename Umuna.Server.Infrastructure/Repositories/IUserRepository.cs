using Umuna.Core.Contracts.Models;
using Umuna.Server.Domain.Entities;

namespace Umuna.Server.Infrastructure.Repositories
{
    public interface IUserRepository
    {
        Task<ServiceResult<User>> GetByIdAsync(int id);
        Task<ServiceResult<List<User>>> GetAllAsync();
        Task<ServiceResult> AddAsync(User user);
        Task<ServiceResult> UpdateAsync(User user);
        Task<ServiceResult> DeleteAsync(User user);
    }
}
