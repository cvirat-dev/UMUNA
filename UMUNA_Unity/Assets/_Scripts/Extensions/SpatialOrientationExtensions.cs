using UMUNA.Assets._Scripts.Extensions;
using UnityEngine;
using UUP.CustomDataTypes;
using SpatialOrientationCore = Umuna.Core.Domain.Data.SpatialOrientation;

namespace UMUNA.Extensions
{
    public static class SpatialOrientationExtensions
    {
        public static void Set(this SpatialOrientation spatialOrientation, SpatialOrientationCore spatialOrientationCore)
        {
            if (spatialOrientationCore == null)
            {
                Debug.LogWarning("SpatialOrientationExtensions.Set: spatialOrientationData is null");
            }
            spatialOrientation.Position.Set(spatialOrientationCore.Position);
            spatialOrientation.Rotation.Set(spatialOrientationCore.Rotation);
        }

        public static SpatialOrientationCore ToSpatialOrientationCore(this SpatialOrientation spatialOrientation)
        {
            if (spatialOrientation == null)
            {
                Debug.LogWarning("SpatialOrientationExtensions.ToSpatialOrientationData: spatialOrientation is null");
                return null;
            }
            return new SpatialOrientationCore
            {
                Position = spatialOrientation.Position.ToVector3Core(),
                Rotation = spatialOrientation.Rotation.ToQuaternionCore()
            };
        }

        public static SpatialOrientation ToSpatialOrientation(this SpatialOrientationCore spatialOrientationData)
        {
            if (spatialOrientationData == null)
            {
                Debug.LogWarning("SpatialOrientationExtensions.ToSpatialOrientation: spatialOrientationData is null");
                return null;
            }
            return new SpatialOrientation
            {
                Position = spatialOrientationData.Position.ToVector3(),
                Rotation = spatialOrientationData.Rotation.ToQuaternion()
            };
        }
    }
}
