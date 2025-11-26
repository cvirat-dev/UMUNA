using Umuna.Core.Contracts.DTOs.User;
using Umuna.Core.Contracts.DTOs.CameraPositions;
using Umuna.Server.Domain.Entities;
using Umuna.Server.Domain.Security;

namespace Umuna.Server.Domain.Mappings
{
    public class UserMapper : IEntityToDtoMapper<User, UserReadDto, UserCreateDto, UserUpdateDto>
    {
        public UserReadDto ToDto(User entity)
        {
            return new UserReadDto
            {
                Id = entity.Id,
                Name = entity.Name,
                Email = entity.Email,
                CreatedAt = entity.CreatedAt,
                UpdatedAt = entity.UpdatedAt ?? entity.CreatedAt,
                CameraPositions = entity.CameraPositions?.Select(cp => new CameraPositionPreviewDto
                {
                    Id = cp.Id,
                    Name = cp.Name
                }).ToList() ?? []
            };
        }

        public List<UserReadDto> ToDtoList(IEnumerable<User> entities)
        {
            return [.. entities.Select(ToDto)];
        }

        public User ToEntity(UserReadDto dto)
        {
            return new User
            {
                Id = dto.Id,
                Name = dto.Name,
                Email = dto.Email,
                PasswordHash = string.Empty,
                CreatedAt = dto.CreatedAt,
                UpdatedAt = dto.UpdatedAt,
                CameraPositions = dto.CameraPositions?.Select(cp => new CameraPosition
                {
                    Id = cp.Id,
                    Name = cp.Name
                }).ToList() ?? []
            };
        }

        public User ToEntity(UserCreateDto dto)
        {
            return new User
            {
                Name = dto.Name,
                Email = dto.Email,
                PasswordHash = PasswordHasher.Hash(dto.Password),
                CreatedAt = DateTime.UtcNow,
                CameraPositions = new List<CameraPosition>()
            };
        }

        public void UpdateEntity(User entity, UserUpdateDto dto)
        {
            entity.Name = dto.Name;
            entity.Email = dto.Email;
            if (!string.IsNullOrWhiteSpace(dto.Password))
            {
                entity.PasswordHash = PasswordHasher.Hash(dto.Password);
            }

            entity.UpdatedAt = DateTime.UtcNow;
        }
    }
}
