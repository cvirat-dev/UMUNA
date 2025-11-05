using System;

namespace Umuna.Core.Common.Math
{
    [Serializable]
    public class SpatialOrientationData
    {
        public string Name { get; set; } = "DefaultSpatialOrientation";
        public Vector3Data Position { get; set; } = Vector3Data.Zero;
        public Vector3Data Rotation { get; set; } = Vector3Data.Zero; // Euler angles

        public SpatialOrientationData() { }

        public SpatialOrientationData(string posName, Vector3Data position, Vector3Data rotation)
        {
            Name = posName;
            Position = position;
            Rotation = rotation;
        }
    }
}