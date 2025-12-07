
using System;
using Umuna.Core.Contracts.Api;
using Umuna.Core.Contracts.Domain.Numerics;

namespace Umuna.Core.Contracts.Api.CameraPositions
{
    public class CameraPositionReadDto : IReadDto
    {
        public int Id { get; set; }              // Server-generated ID
        public string PositionName { get; set; } = null!;
        public Vector3 Position { get; set; } = null!;
        public Quaternion Rotation { get; set; } = null!;
        public DateTime CreatedAt { get; set; }  // Optional: Helpful for UI
        public DateTime UpdatedAt { get; set; }  // Optional: Helpful for UI
        public int UserId { get; set; }          // Associated User ID
    }
}
