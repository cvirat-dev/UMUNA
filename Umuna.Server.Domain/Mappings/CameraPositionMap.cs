using Umuna.Core.Contracts.DTOs.CameraPositions;
using Umuna.Core.Contracts.Models;
using Umuna.Server.Domain.Entities;

namespace Umuna.Server.Domain.Mappings
{
    public static class CameraPositionMap
    {
        public static CameraPositionReadDto ToDto(this CameraPosition entity)
        {
            return new CameraPositionReadDto
            {
                Id = entity.Id,
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
                UserId = entity.UserId
            };
        }

        public static List<CameraPositionReadDto> ToDtoList(this IEnumerable<CameraPosition> entities)
        {
            return [.. entities.Select(e => e.ToDto())];
        }

        public static CameraPosition ToEntity(this CameraPositionReadDto dto)
        {
            return new CameraPosition
            {
                Id = dto.Id,
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
                UserId = dto.UserId
            };
        }

        public static CameraPosition ToEntity(this CameraPositionCreateDto dto)
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
            };
        }

        public static CameraPosition Update(this CameraPosition entity, CameraPositionUpdateDto dto)
        {
            entity.Name = dto.PositionName ?? entity.Name;
            entity.PositionX = dto.Position.X;
            entity.PositionY = dto.Position.Y;
            entity.PositionZ = dto.Position.Z;
            entity.RotationX = dto.Rotation.X;
            entity.RotationY = dto.Rotation.Y;
            entity.RotationZ = dto.Rotation.Z;
            entity.RotationW = dto.Rotation.W;
            return entity;
        }
    }
}
