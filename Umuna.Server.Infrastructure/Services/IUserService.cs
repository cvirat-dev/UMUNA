using Umuna.Core.Contracts.DTOs.User;

namespace Umuna.Server.Infrastructure.Services
{
    public interface IUserService : ICrudService<UserCreateDto, UserUpdateDto, UserReadDto>
    {
    }
}
