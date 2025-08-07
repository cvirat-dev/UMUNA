using System;

namespace Umuna.Core.Data.Common
{
    [Serializable]
    public struct Vector3Data
    {
        public float X { get; set; }
        public float Y { get; set; }
        public float Z { get; set; }

        public Vector3Data(float x, float y, float z)
        {
            X = x;
            Y = y;
            Z = z;
        }

        public static Vector3Data Zero => new Vector3Data(0, 0, 0);
    }
}