
using System;
using Umuna.Core.Contracts.Models;

namespace Umuna.Core.Contracts.DTOs.CameraPositions
{
    public class CameraPositionReadDto
    {
        public int Id { get; set; }              // Server-generated ID
        public string PositionName { get; set; } = null!;
        public PositionDto Position { get; set; }
        public QuaternionDto Rotation { get; set; }
        public DateTime CreatedAt { get; set; }  // Optional: Helpful for UI
        public DateTime UpdatedAt { get; set; }  // Optional: Helpful for UI
        public int UserId { get; set; }          // Associated User ID
    }
}
