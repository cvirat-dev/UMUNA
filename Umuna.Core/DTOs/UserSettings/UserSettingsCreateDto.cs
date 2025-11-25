namespace Umuna.Core.Contracts.DTOs.UserSettings
{
    public class UserSettingsCreateDto
    {
        public float CameraRotationSpeed { get; set; }
        public float CameraTranslationSpeed { get; set; }
        public float CameraZoomSpeed { get; set; }
        public int UserId { get; set; }
    }
}
