using Umuna.Core.Contracts.Models;
using Umuna.Server.Domain.Entities;

namespace Umuna.Server.Infrastructure.Repositories
{
    public interface ICameraPositionRepository
    {
        Task<ServiceResult<CameraPosition>> GetFirstByIdAsync(int id);
        Task<ServiceResult<List<CameraPosition>>> GetAllAsync();
        Task<ServiceResult<List<CameraPosition>>> GetByUserIdAsync(int userId);
        Task<ServiceResult<CameraPosition>> GetDefaultForUserAsync(int userId);
        Task<ServiceResult> AddAsync(CameraPosition cameraPosition);
        Task<ServiceResult> UpdateAsync(CameraPosition cameraPosition);
        Task<ServiceResult> DeleteAsync(CameraPosition cameraPosition);
    }
}
