using Umuna.Core.Contracts.Domain.Numerics;

namespace Umuna.Core.Contracts.Api.CameraPositions
{
    public class CameraPositionCreateDto
    {
        public string PositionName { get; set; } = null!;
        public Vector3 Position { get; set; } = null!;
        public Quaternion Rotation { get; set; } = null!;
    }
}
