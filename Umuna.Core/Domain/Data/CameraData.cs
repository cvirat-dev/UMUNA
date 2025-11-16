
using System;
using System.Collections.Generic;
using Umuna.Core.Domain.Interfaces;

namespace Umuna.Core.Domain.Data
{
    public class CameraData : ICameraData
    {
        #region Fields
        private const int NoCameraSelected = -1;
        #endregion

        #region Properties
        /// <summary>
        /// The list-index of the currently selected camera.
        /// </summary>
        public int CurrentCameraIndex { get; set; } = NoCameraSelected;

        /// <summary>
        /// The last camera position.
        /// </summary>
        public SpatialOrientation LastCameraPosition { get; set; } = new SpatialOrientation();

        /// <summary>
        /// Public read-only access to saved positions.
        /// </summary>
        public List<SpatialOrientation> SavedPositions { get; private set; } = new List<SpatialOrientation>();
        #endregion

        #region Public Methods
        /// <summary>
        /// Adds a new spatial position to the list.
        /// </summary>
        public void AddPosition(SpatialOrientation position)
        {
            if (position == null) throw new ArgumentNullException(nameof(position));
            SavedPositions.Add(position);
        }

        /// <summary>
        /// Updates an existing spatial position at the specified index.
        /// </summary>
        /// <param name="index">The index of the position to update.</param>
        /// <param name="position">The new position data.</param>
        /// <exception cref="ArgumentOutOfRangeException">Raised if the index is out of bounds.</exception>
        /// <exception cref="ArgumentNullException">Raised if the position is null.</exception>
        public void UpdatePosition(int index, SpatialOrientation position)
        {
            if (index < 0 || index >= SavedPositions.Count)
            {
                throw new ArgumentOutOfRangeException(nameof(index), "Invalid index for saved positions.");
            }

            SavedPositions[index] = position ?? throw new ArgumentNullException(nameof(position));
        }

        /// <summary>
        /// Removes a spatial position from the list.
        /// </summary>
        public void RemovePositionAt(int index)
        {
            if (index < 0 || index >= SavedPositions.Count)
            {
                throw new ArgumentOutOfRangeException(nameof(index), "Invalid index for saved positions.");
            }
            SavedPositions.RemoveAt(index);
        }

        /// <summary>
        /// Clears all saved positions.
        /// </summary>
        public void ClearPositions()
        {
            SavedPositions.Clear();
        }

        /// <summary>
        /// Sets the currently selected camera by index.
        /// </summary>
        public void SetSelectedCamera(int cameraIndex)
        {
            if (cameraIndex < 0 || cameraIndex >= SavedPositions.Count)
            {
                throw new ArgumentOutOfRangeException(nameof(cameraIndex), "Invalid camera index.");
            }
            CurrentCameraIndex = cameraIndex;
        }
        #endregion
    }
}
