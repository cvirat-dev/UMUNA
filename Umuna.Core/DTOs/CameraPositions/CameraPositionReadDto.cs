
using System;
using Umuna.Core.Contracts.Models;

namespace Umuna.Core.Contracts.DTOs.CameraPositions
{
    public class CameraPositionReadDto : IReadDto
    {
        public int Id { get; set; }              // Server-generated ID
        public string PositionName { get; set; } = null!;
        public PositionDto Position { get; set; } = null!;
        public QuaternionDto Rotation { get; set; } = null!;
        public DateTime CreatedAt { get; set; }  // Optional: Helpful for UI
        public DateTime UpdatedAt { get; set; }  // Optional: Helpful for UI
        public int UserId { get; set; }          // Associated User ID
    }
}
