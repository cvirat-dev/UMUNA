using Umuna.Core.Contracts.DTOs.UserSettings;
using Umuna.Server.Domain.Entities;

namespace Umuna.Server.Domain.Mappings
{
    public class UserSettingsMapper : IEntityToDtoMapper<
        UserSettings,
        SettingsReadDto,
        SettingsCreateDto,
        SettingsUpdateDto>
    {
        public SettingsReadDto ToDto(UserSettings entity)
        {
            if (entity == null) return null!;

            return new SettingsReadDto
            {
                Id = entity.Id,
                CameraRotationSpeed = entity.CameraRotationSpeed,
                CameraTranslationSpeed = entity.CameraTranslationSpeed,
                CameraZoomSpeed = entity.CameraZoomSpeed,
                CreatedAt = entity.CreatedAt,
                UpdatedAt = entity.UpdatedAt ?? entity.CreatedAt,
                UserId = entity.UserId
            };
        }

        public List<SettingsReadDto> ToDtoList(IEnumerable<UserSettings> entities)
        {
            return entities?.Select(ToDto).ToList() ?? [];
        }

        public UserSettings ToEntity(SettingsReadDto dto)
        {
            if (dto == null) return null!;

            return new UserSettings
            {
                Id = dto.Id,
                CameraRotationSpeed = dto.CameraRotationSpeed,
                CameraTranslationSpeed = dto.CameraTranslationSpeed,
                CameraZoomSpeed = dto.CameraZoomSpeed,
                UpdatedAt = dto.UpdatedAt,
                UserId = dto.UserId
            };
        }

        public UserSettings ToEntity(SettingsCreateDto dto)
        {
            if (dto == null) return null!;

            return new UserSettings
            {
                CameraRotationSpeed = dto.CameraRotationSpeed,
                CameraTranslationSpeed = dto.CameraTranslationSpeed,
                CameraZoomSpeed = dto.CameraZoomSpeed,
                UserId = dto.UserId,
                UpdatedAt = DateTime.UtcNow
            };
        }

        public void UpdateEntity(UserSettings entity, SettingsUpdateDto dto)
        {
            if (entity == null || dto == null) return;

            entity.CameraRotationSpeed = dto.CameraRotationSpeed;
            entity.CameraTranslationSpeed = dto.CameraTranslationSpeed;
            entity.CameraZoomSpeed = dto.CameraZoomSpeed;
            entity.UpdatedAt = DateTime.UtcNow;
        }
    }
}
