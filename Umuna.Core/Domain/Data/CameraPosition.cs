
namespace Umuna.Core.Domain.Data
{
    public class CameraPosition
    {
        // Primary key
        public int Id { get; set; }
        public Vector3Model Position { get; set; }
        public QuaternionModel Rotation { get; set; }

        public CameraPosition() 
        {
            Position = new Vector3Model();
            Rotation = new QuaternionModel();
        }

        public CameraPosition(Vector3Model position, QuaternionModel rotation)
        {
            Position = position;
            Rotation = rotation;
        }
    }

    public class Vector3Model
    {
        public float X { get; set; }
        public float Y { get; set; }
        public float Z { get; set; }

        public Vector3Model() { }

        public Vector3Model(float x, float y, float z)
        {
            X = x;
            Y = y;
            Z = z;
        }
    }

    public class QuaternionModel
    {
        public float X { get; set; }
        public float Y { get; set; }
        public float Z { get; set; }
        public float W { get; set; }

        public QuaternionModel() { }

        public QuaternionModel(float x, float y, float z, float w)
        {
            X = x;
            Y = y;
            Z = z;
            W = w;
        }
    }
}
