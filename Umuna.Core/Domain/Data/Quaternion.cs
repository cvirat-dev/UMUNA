
namespace Umuna.Core.Domain.Data
{
    public class Quaternion
    {
        public float X { get; set; }
        public float Y { get; set; }
        public float Z { get; set; }
        public float W { get; set; }

        public Quaternion() { }

        public Quaternion(float x, float y, float z, float w)
        {
            X = x;
            Y = y;
            Z = z;
            W = w;
        }

        public float[] ToArray()
        {
            return new float[] { X, Y, Z, W };
        }
    }
}
