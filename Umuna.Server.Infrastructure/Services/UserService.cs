using Umuna.Core.Contracts.DTOs.CameraPositions;
using Umuna.Core.Contracts.DTOs.User;
using Umuna.Core.Contracts.Models;
using Umuna.Server.Domain.Entities;
using Umuna.Server.Infrastructure.Repositories;

namespace Umuna.Server.Infrastructure.Services
{
    public class UserService(IUserRepository userRepository) : IUserService
    {
        public async Task<ServiceResult<UserReadDto>> Add(UserCreateDto createDto)
        {
            if (createDto == null)
                return ServiceResult<UserReadDto>.Fail("Create DTO is null.");

            if (string.IsNullOrWhiteSpace(createDto.Name) || 
                string.IsNullOrWhiteSpace(createDto.Email) || 
                string.IsNullOrWhiteSpace(createDto.Password))
                return ServiceResult<UserReadDto>.Fail("Name, email and password are required.");

            try
            {
                var user = new User
                {
                    Name = createDto.Name,
                    Email = createDto.Email,
                    // Note: Password storage currently mirrors received password to match existing simplistic auth logic.
                    PasswordHash = createDto.Password,
                    CreatedAt = DateTime.UtcNow
                };

                await userRepository.Add(user);

                var dto = new UserReadDto
                {
                    Id = user.Id,
                    Name = user.Name,
                    Email = user.Email,
                    CreatedAt = user.CreatedAt,
                    UpdatedAt = DateTime.UtcNow,
                    CameraPositions = user.CameraPositions?.Select(cp => new CameraPositionPreviewDto
                    {
                        Id = cp.Id,
                        Name = cp.Name
                    }).ToList() ?? []
                };

                return ServiceResult<UserReadDto>.Ok(dto);
            }
            catch (Exception ex)
            {
                return ServiceResult<UserReadDto>.Fail(ex.Message);
            }
        }

        public async Task<ServiceResult> Delete(string id)
        {
            if (!int.TryParse(id, out var userId))
                return ServiceResult.Fail("Invalid user ID format.");

            try
            {
                var user = await userRepository.GetById(userId);
                if (user == null)
                    return ServiceResult.Fail("User not found.");

                await userRepository.Delete(userId);
                return ServiceResult.Ok();
            }
            catch (Exception ex)
            {
                return ServiceResult.Fail(ex.Message);
            }
        }

        public async Task<ServiceResult<List<UserReadDto>>> GetAll()
        {
            try
            {
                var users = await userRepository.GetAll();
                var dtos = users?.Select(user => new UserReadDto
                {
                    Id = user.Id,
                    Name = user.Name,
                    Email = user.Email,
                    CreatedAt = user.CreatedAt,
                    UpdatedAt = DateTime.UtcNow,
                    CameraPositions = user.CameraPositions?.Select(cp => new CameraPositionPreviewDto
                    {
                        Id = cp.Id,
                        Name = cp.Name
                    }).ToList() ?? new List<CameraPositionPreviewDto>()
                }).ToList() ?? new List<UserReadDto>();

                return ServiceResult<List<UserReadDto>>.Ok(dtos);
            }
            catch (Exception ex)
            {
                return ServiceResult<List<UserReadDto>>.Fail(ex.Message);
            }
        }

        public async Task<ServiceResult<UserReadDto>> GetById(string id)
        {
            // 1. Validate DTO-level concerns
            if (!int.TryParse(id, out var userId))
            {
                return ServiceResult<UserReadDto>.Fail("Invalid user ID format.");
            }

            // 2. Work with repository using entities
            User user = await userRepository.GetById(userId);
            if(user == null)
            {
                return ServiceResult<UserReadDto>.Fail("User not found.");
            }

            // 3. Apply business logic
            // (e.g., check permissions, enrich data, etc.)

            // 4. Convert entity → DTO for output
            var dto = new UserReadDto
            {
                Id = user.Id,
                Name = user.Name,
                Email = user.Email,
                CreatedAt = user.CreatedAt,
                UpdatedAt = DateTime.UtcNow, // Example; replace with actual updated time if available
                CameraPositions = user.CameraPositions?.Select(cp => new CameraPositionPreviewDto
                {
                    Id = cp.Id,
                    Name = cp.Name,
                }).ToList() ?? []
            };

            return ServiceResult<UserReadDto>.Ok(dto);
        }

        public async Task<ServiceResult<UserReadDto>> Update(string id, UserUpdateDto updateDto)
        {
            // 1. Validate DTO-level concerns
            if (!int.TryParse(id, out var userId))
            {
                return ServiceResult<UserReadDto>.Fail("Invalid user ID format.");
            }

            // 2. Work with repository using entities
            User user = await userRepository.GetById(userId);
            if (user == null)
            {
                return ServiceResult<UserReadDto>.Fail("User not found.");
            }

            // 3. Apply business logic
            user.Name = updateDto.Name ?? user.Name;
            user.Email = updateDto.Email ?? user.Email;
            if (!string.IsNullOrWhiteSpace(updateDto.Password))
            {
                user.PasswordHash = updateDto.Password;
            }

            try
            {
                await userRepository.Update(user);

                var updatedUser = new UserReadDto
                {
                    Id = user.Id,
                    Name = user.Name,
                    Email = user.Email,
                    CreatedAt = user.CreatedAt,
                    UpdatedAt = DateTime.UtcNow, // Example; replace with actual updated time if available
                    CameraPositions = user.CameraPositions?.Select(cp => new CameraPositionPreviewDto
                    {
                        Id = cp.Id,
                        Name = cp.Name,
                    }).ToList() ?? []
                };

                return ServiceResult<UserReadDto>.Ok(updatedUser);
            }
            catch (Exception ex)
            {
                return ServiceResult<UserReadDto>.Fail(ex.Message);
            }
        }
    }
}
