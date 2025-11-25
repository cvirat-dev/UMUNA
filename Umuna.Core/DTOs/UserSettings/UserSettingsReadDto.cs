using System;

namespace Umuna.Core.Contracts.DTOs.UserSettings
{
    public class UserSettingsReadDto
    {
        public int Id { get; set; }
        public float CameraRotationSpeed { get; set; }
        public float CameraTranslationSpeed { get; set; }
        public float CameraZoomSpeed { get; set; }
        public DateTime UpdatedAt { get; set; }
        public int UserId { get; set; }
    }
}
