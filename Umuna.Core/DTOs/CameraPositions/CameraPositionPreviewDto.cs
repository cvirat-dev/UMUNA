
namespace Umuna.Core.Contracts.DTOs.CameraPositions
{
    /// <summary>
    /// The preview DTO for camera positions, containing only essential information.
    /// </summary>
    public class CameraPositionPreviewDto
    {
        public int Id { get; set; }              // Server-generated ID
        public string Name { get; set; } = null!;
    }
}
