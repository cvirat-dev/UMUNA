using System;
using Umuna.Core.Contracts.Models;

namespace Umuna.Core.Contracts.DTOs
{
    public class CameraPositionDto
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string Name { get; set; }  // "Main View", "Top Down", etc.
        public PositionDto Position { get; set; }
        public QuaternionDto Rotation { get; set; }
        public float FieldOfView { get; set; }
        public bool IsDefault { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
