namespace Umuna.Server.Domain.Entities
{
    public class UserSettings : IEntity
    {
        public int Id { get; set; }
        public DateTime CreatedAt { get ; set ; }
        public DateTime? UpdatedAt { get; set; }
        
        // Camera settings
        public float CameraRotationSpeed { get; set; }
        public float CameraTranslationSpeed { get; set; }
        public float CameraZoomSpeed { get; set; }

        // Foreign key
        public int UserId { get; set; }

        // Navigation property
        public User User { get; set; } = null!;
    }
}