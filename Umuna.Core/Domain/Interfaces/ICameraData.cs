using System.Collections.Generic;
using Umuna.Core.Domain.Data;

namespace Umuna.Core.Domain.Interfaces
{
    public interface ICameraData
    {
        int CurrentCameraIndex { get; set; }
        SpatialOrientation LastCameraPosition { get; set; }
        List<SpatialOrientation> SavedPositions { get; }

        void AddPosition(SpatialOrientation position);
        void ClearPositions();
        void RemovePositionAt(int index);
        void SetSelectedCamera(int cameraIndex);
        void UpdatePosition(int index, SpatialOrientation position);
    }
}