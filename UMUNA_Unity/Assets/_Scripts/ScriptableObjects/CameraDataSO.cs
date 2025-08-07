using System;
using Umuna.Core.Data.Umuna;
using UMUNA.Extensions;
using UnityEngine;
using UUP.CustomAttributes.CustomTargets;
using UUP.CustomDataTypes;

namespace UMUNA.ScriptableObjects
{
    [CreateAssetMenu(fileName = "CameraData", menuName = "UMUNA/CameraData", order = 1)]
    public class CameraDataSO : ScriptableObjectT
    {
        public event Action OnCameraDataChanged;

        [Header("Camera Data")]
        [SerializeField] private CameraData cameraData;
        public CameraData CameraData
        {
            get => cameraData;
            set
            {
                cameraData = value;
                OnCameraDataChanged?.Invoke();
            }
        }

        public int Count
        {
            get => cameraData.SavedPositions.Count;
        }

        public SpatialOrientation CurrentCameraPosition
        {
            get => cameraData.SavedPositions[cameraData.CurrentCameraIndex].ToSpatialOrientation();
        }

        public int CurrentCameraIndex
        {
            get => cameraData.CurrentCameraIndex;
            set
            {
                if (value == cameraData.CurrentCameraIndex) return;

                if (value >= -1 && value < cameraData.SavedPositions.Count)
                {
                    cameraData.CurrentCameraIndex = value;
                    OnCameraDataChanged?.Invoke();
                }
                else
                {
                    throw new IndexOutOfRangeException("Index out of range");
                }
            }
        }

        private void OnEnable()
        {
            // Initialize the data if needed
            if (cameraData == null)
            {
                cameraData = new CameraData();
                Debug.Log("CameraData initialized");
            }
        }

        public void SetNextCameraIndex()
        {
            cameraData.SetNextCameraIndex();
            OnCameraDataChanged?.Invoke();
        }

        public void ModifySavedPositions(ListOperation op, SpatialOrientation spatialOrientation)
        {
            cameraData.ModifySavedPositions(op, spatialOrientation.ToSpatialOrientationData());
            OnCameraDataChanged?.Invoke();
        }
    }
}
