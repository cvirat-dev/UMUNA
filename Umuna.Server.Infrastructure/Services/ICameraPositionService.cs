using Umuna.Core.Contracts.DTOs.CameraPositions;
using Umuna.Core.Contracts.Models;

namespace Umuna.Server.Infrastructure.Services
{
    public interface ICameraPositionService : ICrudService<
        CameraPositionCreateDto, 
        CameraPositionUpdateDto, 
        CameraPositionReadDto>
    {
        Task<ServiceResult<List<CameraPositionReadDto>>> GetByUserId(int userId);
        Task<ServiceResult<CameraPositionReadDto>> GetDefaultForUser(int userId);
    }
}
