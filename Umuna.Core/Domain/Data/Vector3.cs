
namespace Umuna.Core.Domain.Data
{
    public class Vector3
    {
        public float X { get; set; }
        public float Y { get; set; }
        public float Z { get; set; }

        public Vector3() { }

        public Vector3(float x, float y, float z)
        {
            X = x;
            Y = y;
            Z = z;
        }

        public float[] ToArray()
        {
            return new float[] { X, Y, Z };
        }
    }
}
