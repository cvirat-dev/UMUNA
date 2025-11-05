using System.Collections.Generic;
using Umuna.Core.Common.Math;

namespace Umuna.Core.Domain.Interfaces
{
    public interface ICameraData
    {
        int CurrentCameraIndex { get; set; }
        SpatialOrientationData LastCameraPosition { get; set; }
        List<SpatialOrientationData> SavedPositions { get; }

        void AddPosition(SpatialOrientationData position);
        void ClearPositions();
        void RemovePositionAt(int index);
        void SetSelectedCamera(int cameraIndex);
        void UpdatePosition(int index, SpatialOrientationData position);
    }
}