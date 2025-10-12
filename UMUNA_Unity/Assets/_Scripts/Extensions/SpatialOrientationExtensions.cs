using Umuna.Core.SharedData.Common;
using UnityEngine;
using UUP.CustomDataTypes;

namespace UMUNA.Extensions
{
    public static class SpatialOrientationExtensions
    {
        public static void Set(this SpatialOrientation spatialOrientation, SpatialOrientationData spatialOrientationData)
        {
            if (spatialOrientationData == null)
            {
                Debug.LogWarning("SpatialOrientationExtensions.Set: spatialOrientationData is null");
            }
            spatialOrientation.Position.Set(spatialOrientationData.Position);
            spatialOrientation.Rotation = Quaternion.Euler(spatialOrientationData.Rotation.ToVector3());
        }

        public static SpatialOrientationData ToSpatialOrientationData(this SpatialOrientation spatialOrientation)
        {
            if (spatialOrientation == null)
            {
                Debug.LogWarning("SpatialOrientationExtensions.ToSpatialOrientationData: spatialOrientation is null");
                return null;
            }
            return new SpatialOrientationData
            {
                Position = new Vector3Data(
                    spatialOrientation.Position.x,
                    spatialOrientation.Position.y,
                    spatialOrientation.Position.z
                    ),
                Rotation = new Vector3Data(
                    spatialOrientation.Rotation.eulerAngles.x,
                    spatialOrientation.Rotation.eulerAngles.y,
                    spatialOrientation.Rotation.eulerAngles.z
                    )
            };
        }

        public static SpatialOrientation ToSpatialOrientation(this SpatialOrientationData spatialOrientationData)
        {
            if (spatialOrientationData == null)
            {
                Debug.LogWarning("SpatialOrientationExtensions.ToSpatialOrientation: spatialOrientationData is null");
                return null;
            }
            return new SpatialOrientation
            {
                Position = new Vector3(
                    spatialOrientationData.Position.X,
                    spatialOrientationData.Position.Y,
                    spatialOrientationData.Position.Z
                    ),
                Rotation = Quaternion.Euler(
                    spatialOrientationData.Rotation.X,
                    spatialOrientationData.Rotation.Y,
                    spatialOrientationData.Rotation.Z
                    )
            };
        }
    }
}
