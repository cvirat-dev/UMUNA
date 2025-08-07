using UUP.CustomAttributes;
using UUP.CustomAttributes.CustomTargets;
using UUP.CustomDataTypes;
using UUP.ScriptableObjects.Data.ObervableLists;
using UUP.ScriptableObjects.Data.Variables;
using UUP.ScriptableObjects.GameEvents.Serialized.CustomSerializables;
using UnityEngine;
using UMUNA.EventManagement;

namespace UMUNA._Scripts.CameraObservableList
{
    public class CameraPositionController : MonoBehaviourT
    {
        private enum Mode
        {
            Loop,
            SingleSequence,
            Random
        }

        [SerializeField]
        private readonly SpatialOrientationObservableListSO spatialPositions;

        [SerializeField]
        private readonly IntVariableSO currentLocationIndex;

        [SerializeField]
        private readonly Mode mode;

        public SpatialOrientationGameEvent OnTriggerNextGE;

        private void Start()
        {
            currentLocationIndex.Value = -1; // not set

            if (spatialPositions == null)
                Debug.LogError("PosRotArraySO is not assigned in " + name);

            EventManager.Ui.OnEmptyList.AddListener(ResetCameraIndex);
            EventManager.Ui.OnResetDefaultCameraPosition.AddListener(ResetCameraIndex);
            EventManager.Ui.OnNextCameraPosition.AddListener(SetNextCameraPosition);
            EventManager.CameraPosition.OnNewCameraPositionAt.AddListener(SetCameraPositionAtIndex);

        }

        private void OnDisable()
        {
            EventManager.Ui.OnEmptyList.RemoveListener(ResetCameraIndex);
            EventManager.Ui.OnResetDefaultCameraPosition.RemoveListener(ResetCameraIndex);
            EventManager.Ui.OnNextCameraPosition.RemoveListener(SetNextCameraPosition);
            EventManager.CameraPosition.OnNewCameraPositionAt.RemoveListener(SetCameraPositionAtIndex);
        }

        [InspectorButton]
        public void SetNextCameraPosition()
        {
            Debug.Log("SetNextCameraPosition");
            int numberOfSetPositions = spatialPositions.Count;
            SpatialOrientation spatialPosition = null;

            if (numberOfSetPositions == 0)
            {
                currentLocationIndex.Value = -1;
                Debug.LogWarning("No positions defined in " + name);
                return;
            }

            else if (numberOfSetPositions == 1)
            {
                var index = 1;
                spatialPosition = spatialPositions.GetAt(index);
                currentLocationIndex.Value = index;
                OnTriggerNextGE.Raise(spatialPosition);
                Debug.LogWarning("Only one position defined in " + name);
                return;
            }

            if (mode == Mode.Loop)
            {
                int nextIndex = currentLocationIndex.Value + 1;

                if (nextIndex >= spatialPositions.Count)
                {
                    Debug.Log("End of sequence reached, restart because Loop-Mode.");
                    nextIndex = 0;
                }

                currentLocationIndex.Value = nextIndex;
                spatialPosition = spatialPositions.GetAt(nextIndex);
                OnTriggerNextGE.Raise(spatialPosition);
            }
            else if (mode == Mode.SingleSequence)
            {
                int nextIndex = currentLocationIndex.Value + 1;

                if (nextIndex >= spatialPositions.Count)
                {
                    Debug.Log("End of sequence reached, return because SingleSequence-Mode.");
                    return;
                }

                currentLocationIndex.Value = nextIndex;
                spatialPosition = spatialPositions.GetAt(nextIndex);
                OnTriggerNextGE.Raise(spatialPosition);
            }
            else
            {
                // Random-Mode
                int nextIndex = Random.Range(0, spatialPositions.Count);
                spatialPosition = spatialPositions.GetAt(nextIndex);
                OnTriggerNextGE.Raise(spatialPosition);
            }
        }

        [InspectorButton]
        public void SetCameraPositionAtIndex(int index)
        {
            if (index < 0 || index >= spatialPositions.Count)
            {
                Debug.LogWarning("Index out of range in " + name);
                return;
            }

            currentLocationIndex.Value = index;
            var spatialPosition = spatialPositions.GetAt(index);
            OnTriggerNextGE.Raise(spatialPosition);
        }

        [InspectorButton]
        public void ResetCameraIndex()
        {
            currentLocationIndex.Value = -1;
        }

    }
}