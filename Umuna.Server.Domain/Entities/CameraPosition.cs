
namespace Umuna.Server.Domain.Entities
{
    /// <summary>
    /// The CameraPosition entity represents a saved camera position and orientation for a user.
    /// This DB-Entity still stores individual float properties for position and rotation to optimize querying and indexing.
    /// </summary>
    public class CameraPosition : IEntityDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public float PositionX { get; set; }
        public float PositionY { get; set; }
        public float PositionZ { get; set; }
        public float RotationX { get; set; }
        public float RotationY { get; set; }
        public float RotationZ { get; set; }
        public float RotationW { get; set; }
        public float FieldOfView { get; set; }
        public bool IsDefault { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

        // Foreign key
        public int UserId { get; set; }

        // Navigation property
        public User User { get; set; } = null!;

    }
}