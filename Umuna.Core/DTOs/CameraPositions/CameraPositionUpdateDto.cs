using Umuna.Core.Contracts.Models;

namespace Umuna.Core.Contracts.DTOs.CameraPositions
{
    public class CameraPositionUpdateDto
    {
        // ID comes from the route
        public string PositionName { get; set; } = null!;
        public PositionDto Position { get; set; } = null!;
        public QuaternionDto Rotation { get; set; } = null!;
    }
}
