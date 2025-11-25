using Umuna.Core.Contracts.DTOs.User;
using Umuna.Core.Contracts.Models;
using Umuna.Server.Infrastructure.Repositories;
using Umuna.Server.Domain.Entities;

namespace Umuna.Server.Infrastructure.Services
{
    public class UserService(IUserRepository userRepository) : IUserService
    {
        private readonly IUserRepository _userRepository = userRepository;

        public async Task<ServiceResult<UserCreateDto>> AddUserAsync(UserCreateDto userDto)
        {
            var user = new User
            {
                Name = userDto.Name,
                Email = userDto.Email,
                PasswordHash = userDto.Password, // In production, hash this!
                CreatedAt = DateTime.UtcNow
            };

            var result = await _userRepository.AddAsync(user);
            
            if (!result.Success)
            {
                return ServiceResult<UserCreateDto>.Fail(result.Message ?? "Failed to add user");
            }

            return ServiceResult<UserCreateDto>.Ok(userDto);
        }

        public async Task<ServiceResult<UserCreateDto>> GetUserByIdAsync(string id)
        {
            if (!int.TryParse(id, out int userId))
            {
                return ServiceResult<UserCreateDto>.Fail("Invalid user ID");
            }

            var result = await _userRepository.GetByIdAsync(userId);
            
            if (!result.Success || result.Data == null)
            {
                return ServiceResult<UserCreateDto>.Fail(result.Message ?? "User not found");
            }

            var userDto = new UserCreateDto
            {
                Name = result.Data.Name,
                Email = result.Data.Email,
                Password = string.Empty // Never return password
            };

            return ServiceResult<UserCreateDto>.Ok(userDto);
        }
    }
}
