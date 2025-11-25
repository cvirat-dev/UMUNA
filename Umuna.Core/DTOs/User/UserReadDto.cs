using System;
using System.Collections.Generic;
using Umuna.Core.Contracts.DTOs.CameraPositions;

namespace Umuna.Core.Contracts.DTOs.User
{
    public class UserReadDto : IReadDto
    {
        public int Id { get; set; }              // Server-generated ID
        public string Name { get; set; } = null!;
        public string Email { get; set; } = null!;
        public DateTime CreatedAt { get; set; }  // Optional: Helpful for UI
        public DateTime UpdatedAt { get; set; }  // Optional: Helpful for UI
        public List<CameraPositionPreviewDto> CameraPositions { get; set; } = new List<CameraPositionPreviewDto>();
    }
}
