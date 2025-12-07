namespace Umuna.Core.Contracts.Api.UserSettings
{
    public class SettingsCreateDto
    {
        public float CameraRotationSpeed { get; set; }
        public float CameraTranslationSpeed { get; set; }
        public float CameraZoomSpeed { get; set; }
        public int UserId { get; set; }
    }
}
