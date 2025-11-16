
namespace Umuna.Core.Domain.Data
{
    public class CameraPosition
    {
        // Primary key
        public int Id { get; set; }
        public Vector3 Position { get; set; }
        public Quaternion Rotation { get; set; }

        public CameraPosition() 
        {
            Position = new Vector3();
            Rotation = new Quaternion();
        }

        public CameraPosition(Vector3 position, Quaternion rotation)
        {
            Position = position;
            Rotation = rotation;
        }
    }
}
