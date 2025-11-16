using System;

namespace Umuna.Core.Domain.Data
{
    public class SpatialOrientation
    {
        public Vector3 Position { get; set; }
        public Quaternion Rotation { get; set; }
        public SpatialOrientation()
        {
            Position = new Vector3();
            Rotation = new Quaternion();
        }
        public SpatialOrientation(Vector3 position, Quaternion rotation)
        {
            Position = position;
            Rotation = rotation;
        }

        public SpatialOrientation(float[] positions, float[] rotations)
        {
            if (positions.Length != 3)
                throw new ArgumentException("Positions array must have exactly 3 elements.");
            if (rotations.Length != 4)
                throw new ArgumentException("Rotations array must have exactly 4 elements.");
            Position = new Vector3(positions[0], positions[1], positions[2]);
            Rotation = new Quaternion(rotations[0], rotations[1], rotations[2], rotations[3]);
        }
    }
}