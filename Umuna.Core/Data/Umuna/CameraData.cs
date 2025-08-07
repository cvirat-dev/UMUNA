
using System;
using System.Collections.Generic;
using Umuna.Core.Data.Common;

namespace Umuna.Core.Data.Umuna
{
    [Serializable]
    public class CameraData
    {
        public SpatialOrientationData LastCameraPosition { get; set; } = new SpatialOrientationData();
        public int CurrentCameraIndex { get; set; }
        public List<SpatialOrientationData> SavedPositions { get; set; } = new List<SpatialOrientationData>();

        public void SetNextCameraIndex()
        {
            CurrentCameraIndex++;
            if (CurrentCameraIndex >= SavedPositions.Count)
            {
                CurrentCameraIndex = 0;
            }
        }

        public void ModifySavedPositions(ListOperation op, params object[] param)
        {
            switch (op)
            {
                case ListOperation.Add:
                    if (param.Length == 1 && param[0] is SpatialOrientationData spAdd)
                    {
                        SavedPositions.Add(spAdd);
                    }
                    else
                    {
                        throw new ArgumentException($"Invalid parameters for Add operation. Expected a {nameof(SpatialOrientationData)} object.");
                    }
                        break;
                case ListOperation.Update:
                    if (param.Length == 2 && param[0] is SpatialOrientationData spUpdate && param[1] is int index && index >= 0 && index < SavedPositions.Count)
                    {
                        SavedPositions[index] = spUpdate;
                        CurrentCameraIndex = index;
                    }
                    else
                    {
                        throw new ArgumentException($"Invalid parameters for Update operation. Expected a {nameof(SpatialOrientationData)} object and a valid index.");
                    }
                        break;
                case ListOperation.Remove:
                    if (param.Length == 1 && param[0] is int indexRemove && indexRemove >= 0 && indexRemove < SavedPositions.Count)
                    {
                        SavedPositions.RemoveAt(indexRemove);
                        if (CurrentCameraIndex >= SavedPositions.Count)
                        {
                            CurrentCameraIndex = SavedPositions.Count - 1;
                        }
                    }
                    else
                    {
                        throw new ArgumentException($"Invalid parameters for Remove operation. Expected a valid index.");
                    }
                    break;
                case ListOperation.Clear:
                    SavedPositions.Clear();
                    CurrentCameraIndex = -1;
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(op), op, null);
            }
        }

        public void AddPosition(SpatialOrientationData position)
        {
            SavedPositions.Add(position);
        }

        public void UpdateSavedPosition(int index, SpatialOrientationData position)
        {
            if (index >= 0 && index < SavedPositions.Count)
            {
                SavedPositions[index] = position;
                CurrentCameraIndex = index;
            }
        }

        public void RemovePosition(int index)
        {
            if (index >= 0 && index < SavedPositions.Count)
            {
                SavedPositions.RemoveAt(index);
                if (CurrentCameraIndex >= SavedPositions.Count)
                {
                    CurrentCameraIndex = SavedPositions.Count - 1;
                }
            }
        }

        public void Clear()
        {
            SavedPositions.Clear();
            CurrentCameraIndex = -1;
        }
    }
}
