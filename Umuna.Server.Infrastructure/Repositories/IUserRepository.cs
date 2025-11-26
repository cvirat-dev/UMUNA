using Umuna.Server.Domain.Entities;

namespace Umuna.Server.Infrastructure.Repositories
{
    public interface IUserRepository : ICrudRepository<User>
    {
        Task<User?> GetByName(string username);
    }
}
