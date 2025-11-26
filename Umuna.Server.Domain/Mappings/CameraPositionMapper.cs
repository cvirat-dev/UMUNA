using Umuna.Core.Contracts.DTOs.CameraPositions;
using Umuna.Core.Contracts.Models;
using Umuna.Server.Domain.Entities;

namespace Umuna.Server.Domain.Mappings
{
    public class CameraPositionMapper : IEntityToDtoMapper<CameraPosition, CameraPositionReadDto, CameraPositionCreateDto, CameraPositionUpdateDto>
    {
        public CameraPositionReadDto ToDto(CameraPosition entity)
        {
            return new CameraPositionReadDto
            {
                Id = entity.Id,
                UserId = entity.UserId,
                PositionName = entity.Name,
                Position = new PositionDto
                {
                    X = entity.PositionX,
                    Y = entity.PositionY,
                    Z = entity.PositionZ
                },
                Rotation = new QuaternionDto
                {
                    X = entity.RotationX,
                    Y = entity.RotationY,
                    Z = entity.RotationZ,
                    W = entity.RotationW
                },
                CreatedAt = entity.CreatedAt,
                UpdatedAt = entity.UpdatedAt ?? entity.CreatedAt,
            };
        }
        
        public List<CameraPositionReadDto> ToDtoList(IEnumerable<CameraPosition> entities)
        {
            return entities.Select(e => ToDto(e)).ToList();
        }
        
        public CameraPosition ToEntity(CameraPositionReadDto dto)
        {
            return new CameraPosition
            {
                Id = dto.Id,
                UserId = dto.UserId,
                Name = dto.PositionName,
                PositionX = dto.Position.X,
                PositionY = dto.Position.Y,
                PositionZ = dto.Position.Z,
                RotationX = dto.Rotation.X,
                RotationY = dto.Rotation.Y,
                RotationZ = dto.Rotation.Z,
                RotationW = dto.Rotation.W,
                CreatedAt = dto.CreatedAt,
                UpdatedAt = dto.UpdatedAt,
            };
        }
        
        public CameraPosition ToEntity(CameraPositionCreateDto dto)
        {
            return new CameraPosition
            {
                Name = dto.PositionName,
                PositionX = dto.Position.X,
                PositionY = dto.Position.Y,
                PositionZ = dto.Position.Z,
                RotationX = dto.Rotation.X,
                RotationY = dto.Rotation.Y,
                RotationZ = dto.Rotation.Z,
                RotationW = dto.Rotation.W,
                CreatedAt = DateTime.UtcNow,
            };
        }
        
        public void UpdateEntity(CameraPosition entity, CameraPositionUpdateDto dto)
        {
            entity.Name = dto.PositionName;
            entity.PositionX = dto.Position.X;
            entity.PositionY = dto.Position.Y;
            entity.PositionZ = dto.Position.Z;
            entity.RotationX = dto.Rotation.X;
            entity.RotationY = dto.Rotation.Y;
            entity.RotationZ = dto.Rotation.Z;
            entity.RotationW = dto.Rotation.W;
            entity.UpdatedAt = DateTime.UtcNow;
        }
    }
}
