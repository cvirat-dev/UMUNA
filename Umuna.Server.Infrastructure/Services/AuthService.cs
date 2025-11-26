using Umuna.Core.Contracts.DTOs.Requests;
using Umuna.Core.Contracts.DTOs.User;
using Umuna.Core.Contracts.Models;
using Umuna.Server.Domain.Entities;
using Umuna.Server.Domain.Mappings;
using Umuna.Server.Domain.Security;
using Umuna.Server.Infrastructure.Repositories;

namespace Umuna.Server.Infrastructure.Services
{
    public class AuthService(
        IUserRepository userRepository,
        IEntityToDtoMapper<User, UserReadDto, UserCreateDto, UserUpdateDto> entityToDtoMapper) : IAuthService
    {
        private readonly IUserRepository _userRepository = userRepository;
        private readonly IEntityToDtoMapper<User, UserReadDto, UserCreateDto, UserUpdateDto> _entityToDtoMapper = entityToDtoMapper;

        public async Task<ServiceResult<UserReadDto>> Authenticate(LoginRequestDto loginRequestDto)
        {
            try
            {
                User? user = await _userRepository.GetByName(loginRequestDto.UserName);

                if (user == null)
                    return ServiceResult<UserReadDto>.Fail("Invalid credentials.");

                if (!PasswordHasher.Verify(user.PasswordHash, loginRequestDto.Password))
                    return ServiceResult<UserReadDto>.Fail("Invalid credentials.");

                UserReadDto userDto = _entityToDtoMapper.ToDto(user);
                return ServiceResult<UserReadDto>.Ok(userDto);
            }
            catch (Exception ex)
            {
                return ServiceResult<UserReadDto>.Fail($"An error occurred during authentication: {ex.Message}");
            }
        }

    }
}
