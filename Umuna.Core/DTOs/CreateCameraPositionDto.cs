using Umuna.Core.Contracts.Models;

namespace Umuna.Core.Contracts.DTOs
{
    public class CreateCameraPositionDto
    {
        public string Name { get; set; }
        public PositionDto Position { get; set; }
        public QuaternionDto Rotation { get; set; }
        public float FieldOfView { get; set; }
        public bool IsDefault { get; set; }
    }
}
