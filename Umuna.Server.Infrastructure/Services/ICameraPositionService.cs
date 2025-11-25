using Umuna.Core.Contracts.DTOs.CameraPositions;
using Umuna.Core.Contracts.Models;

namespace Umuna.Server.Infrastructure.Services
{
    public interface ICameraPositionService : ICrudService<
        CameraPositionCreateDto, 
        CameraPositionUpdateDto, 
        CameraPositionReadDto>
    {
        Task<ServiceResult<List<CameraPositionReadDto>>> GetByUserId(string userId);
        Task<ServiceResult<CameraPositionReadDto>> GetDefaultForUser(string userId);
    }
}
