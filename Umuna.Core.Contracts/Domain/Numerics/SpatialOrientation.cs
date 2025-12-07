
namespace Umuna.Core.Contracts.Domain.Numerics
{
    public class SpatialOrientation
    {
        public Vector3 Position { get; set; } = new Vector3();
        public Quaternion Rotation { get; set; } = new Quaternion();

        public SpatialOrientation()
        {
        }

        public SpatialOrientation(Vector3 position, Quaternion rotation)
        {
            Position = position;
            Rotation = rotation;
        }
    }
}
