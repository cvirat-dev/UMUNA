namespace Umuna.Server.Domain.Entities
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; } = null!;
        public DateTime CreatedAt { get; set; }

        // Navigation properties (Auto-handled by EF Core)
        public UserSettings Settings { get; set; }

        public List<CameraPosition> CameraPositions { get; set; } = new();
    }
}
