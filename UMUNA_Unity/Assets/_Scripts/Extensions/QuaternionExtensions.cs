using UnityEngine;
using QuaternionCore = Umuna.Core.Domain.Data.Quaternion;

namespace UMUNA.Assets._Scripts.Extensions
{
    public static class QuaternionExtensions
    {
        public static void Set(this Quaternion quaternion, QuaternionCore quaternionCore)
        {
            quaternion.x = quaternionCore.X;
            quaternion.y = quaternionCore.Y;
            quaternion.z = quaternionCore.Z;
            quaternion.w = quaternionCore.W;
        }

        public static Quaternion ToQuaternion(this QuaternionCore quaternionCore)
        {
            return new Quaternion(
                quaternionCore.X,
                quaternionCore.Y,
                quaternionCore.Z,
                quaternionCore.W
            );
        }

        public static QuaternionCore ToQuaternionCore(this Quaternion quaternion)
        {
            return new QuaternionCore
            {
                X = quaternion.x,
                Y = quaternion.y,
                Z = quaternion.z,
                W = quaternion.w
            };
        }

        public static (float X, float Y, float Z, float W) ToTuple(this Quaternion quaternion)
        {
            return (quaternion.x, quaternion.y, quaternion.z, quaternion.w);
        }

    }
}