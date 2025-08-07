using Umuna.Core.Data.Common;
using UnityEngine;

namespace UMUNA.Extensions
{
    public static class Vector3Extensions
    {
        public static void Set(this Vector3 vector, Vector3Data vectorData)
        {
            vector.x = vectorData.X;
            vector.y = vectorData.Y;
            vector.z = vectorData.Z;
        }
        
        public static Vector3 ToVector3(this Vector3Data vectorData)
        {
            return new Vector3(vectorData.X, vectorData.Y, vectorData.Z);
        }

        public static (float X, float Y, float Z) ToTuple(this Vector3 vector)
        {
            return (vector.x, vector.y, vector.z);
        }
    }
}
