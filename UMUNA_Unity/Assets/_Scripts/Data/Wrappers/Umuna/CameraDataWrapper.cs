using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Umuna.Core.SharedData.Common;
using Umuna.Core.SharedData.Umuna;
using UMUNA.Extensions;
using UUP.CustomDataTypes;

namespace UMUNA.Data.Wrappers.Umuna
{
    public class CameraDataWrapper : WrapperBase<CameraData>, IDisposable, ICameraData
    {
        #region Fields
        private readonly ObservableCollection<SpatialOrientation> _savedPositions;
        #endregion

        #region Properties
        public SpatialOrientation LastCameraPosition
        {
            get => Model.LastCameraPosition.ToSpatialOrientation();
            set
            {
                Model.LastCameraPosition = value.ToSpatialOrientationData();
                NotifyChange();
            }
        }

        public int CurrentCameraIndex
        {
            get => Model.CurrentCameraIndex;
            set
            {
                Model.CurrentCameraIndex = value;
                NotifyChange();
            }
        }

        public IReadOnlyList<SpatialOrientation> SavedPositions => _savedPositions;
        List<SpatialOrientationData> ICameraData.SavedPositions => Model.SavedPositions;
        SpatialOrientationData ICameraData.LastCameraPosition
        {
            get => Model.LastCameraPosition;
            set
            {
                Model.LastCameraPosition = value;
                NotifyChange();
            }
        }
        #endregion

        public CameraDataWrapper(CameraData model) : base(model)
        {
            if (model == null)
                throw new ArgumentNullException(nameof(model), "CameraData cannot be null.");

            _savedPositions = new(model.SavedPositions.Select(sp => sp.ToSpatialOrientation()));
        }

        // Rebuild the internal collection from the model (call this after any model change)
        private void RebuildFromModel()
        {
            _savedPositions.Clear();
            foreach (var sd in Model.SavedPositions)
                _savedPositions.Add(sd.ToSpatialOrientation());
        }

        public void ClearPositions()
        {
            Model.ClearPositions();
            RebuildFromModel();
            NotifyChange();
        }

        public void Dispose()
        {
            // nothing subscribed here, but if you add event handlers later, unsubscribe here
        }

        public void AddPosition(SpatialOrientation pos)
        {
            AddPosition(pos.ToSpatialOrientationData());
        }

        public void AddPosition(SpatialOrientationData position)
        {
            if (position == null)
                throw new ArgumentNullException(nameof(position), "Position cannot be null.");
            Model.AddPosition(position);
            RebuildFromModel();
            NotifyChange();
        }

        public void RemovePositionAt(int index)
        {
            Model.RemovePositionAt(index);
            RebuildFromModel();
            NotifyChange();
        }

        public void SetSelectedCamera(int cameraIndex)
        {
            if (cameraIndex < 0 || cameraIndex >= _savedPositions.Count)
                throw new ArgumentOutOfRangeException(nameof(cameraIndex), "Camera index is out of range.");
            CurrentCameraIndex = cameraIndex;
            LastCameraPosition = _savedPositions[cameraIndex];
            NotifyChange();
        }

        public void UpdatePosition(int index, SpatialOrientation pos)
        {
            if (pos == null)
                throw new ArgumentNullException(nameof(pos), "Position cannot be null.");
            UpdatePosition(index, pos.ToSpatialOrientationData());
        }

        public void UpdatePosition(int index, SpatialOrientationData position)
        {
            if (index < 0 || index >= _savedPositions.Count)
            {
                throw new ArgumentOutOfRangeException(nameof(index), "Invalid index for saved positions.");
            }
            if (position == null)
            {
                throw new ArgumentNullException(nameof(position), "Position cannot be null.");
            }
            Model.UpdatePosition(index, position);
            RebuildFromModel();
            NotifyChange();
        }
    }
}
