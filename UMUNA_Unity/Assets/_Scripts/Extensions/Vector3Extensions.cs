using UnityEngine;
using Vector3Core = Umuna.Core.Domain.Data.Vector3;

namespace UMUNA.Extensions
{
    public static class Vector3Extensions
    {
        public static void Set(this Vector3 vector, Vector3Core vectorData)
        {
            vector.x = vectorData.X;
            vector.y = vectorData.Y;
            vector.z = vectorData.Z;
        }
        
        public static Vector3 ToVector3(this Vector3Core vectorData)
        {
            return new Vector3(vectorData.X, vectorData.Y, vectorData.Z);
        }

        public static Vector3Core ToVector3Core(this Vector3 vector)
        {
            return new Vector3Core
            {
                X = vector.x,
                Y = vector.y,
                Z = vector.z
            };
        }

        public static (float X, float Y, float Z) ToTuple(this Vector3 vector)
        {
            return (vector.x, vector.y, vector.z);
        }
    }
}
