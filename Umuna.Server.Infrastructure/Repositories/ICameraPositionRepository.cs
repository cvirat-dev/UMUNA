using Umuna.Server.Domain.Entities;

namespace Umuna.Server.Infrastructure.Repositories
{
    public interface ICameraPositionRepository : ICrudRepository<CameraPosition>
    {
        Task<List<CameraPosition>> GetByUserId(int userId);
        Task<CameraPosition?> GetDefaultForUser(int userId);
    }
}
