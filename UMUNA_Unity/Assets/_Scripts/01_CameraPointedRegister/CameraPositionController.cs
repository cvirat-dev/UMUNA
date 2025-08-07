using UUP.CustomAttributes;
using UUP.CustomAttributes.CustomTargets;
using UUP.CustomDataTypes;
using UUP.ScriptableObjects.Data.Entries.NotifiedRegisters;
using UUP.ScriptableObjects.Data.Variables;
using UUP.ScriptableObjects.GameEvents.Serialized.CustomSerializables;
using UnityEngine;

namespace UMUNA._Scripts.CameraPositionNotifRegister
{
    internal enum Mode
    {
        Loop,
        SingleSequence,
        Random
    }
    public class CameraPositionsController : MonoBehaviourT
    {
        [SerializeField]
        private SpatialOrientationNotifRegister spatialPositions;

        [SerializeField]
        private IntVariableSO currentLocationIndex;

        [SerializeField]
        private Mode mode;

        public SpatialOrientationGameEvent OnTriggerNext;

        private void Start()
        {
            currentLocationIndex.Value = -1; // not set

            if (spatialPositions == null)
            {
                Debug.LogError("PosRotArraySO is not assigned in " + name);
            }

            spatialPositions.SetEntriesID();
        }

        [InspectorButton]
        public void SetNextCameraPosition()
        {
            Debug.Log("SetNextCameraPosition");
            int numberOfSetPositions = spatialPositions.NumberOfDefinedEntries;
            SpatialOrientation spatialPosition = null;
            bool loopMode;

            if (numberOfSetPositions == 0)
            {
                currentLocationIndex.Value = -1;
                Debug.LogWarning("No positions defined in " + name);
                return;
            }

            else if (numberOfSetPositions == 1)
            {
                var index = spatialPositions.GetIndexOfFirstDefinedEntry();
                spatialPosition = spatialPositions.GetAt(index);
                currentLocationIndex.Value = index;
                OnTriggerNext.Raise(spatialPosition);
                Debug.LogWarning("Only one position defined in " + name);
                return;
            }

            if (mode == Mode.Loop)
            {
                loopMode = true;
                int nextIndex = spatialPositions.GetIndexOfNextDefinedEntry(currentLocationIndex.Value, loopMode);

                if (nextIndex == -1)
                {
                    Debug.LogWarning("End of sequence reached in " + name);
                    return;
                }

                currentLocationIndex.Value = nextIndex;
                spatialPosition = spatialPositions.GetAt(nextIndex);
                OnTriggerNext.Raise(spatialPosition);
            }
            else if (mode == Mode.SingleSequence)
            {
                int currentIndex = currentLocationIndex.Value;
                loopMode = false;
                int nextIndex = spatialPositions.GetIndexOfNextDefinedEntry(currentIndex, loopMode);

                if (nextIndex == -1)
                {
                    Debug.LogWarning("End of sequence reached in " + name);
                    return;
                }

                currentLocationIndex.Value = nextIndex;
                spatialPosition = spatialPositions.GetAt(nextIndex);
                OnTriggerNext.Raise(spatialPosition);
            }
            else
            {
                int[] existingEntries = spatialPositions.GetIndexesOfDefinedEntries();

                if (existingEntries.Length == 0)
                {
                    Debug.LogWarning("No positions defined in " + name);
                    return;
                }

                // Choose a random index from the existing entries
                var randomIndex = Random.Range(0, existingEntries.Length);
                var randomEntryIndex = existingEntries[randomIndex];

                spatialPosition = spatialPositions.GetAt(randomEntryIndex);
                currentLocationIndex.Value = randomEntryIndex;
                OnTriggerNext.Raise(spatialPosition);
            }
        }

        public void ResetCameraIndex()
        {
            currentLocationIndex.Value = -1;
        }

    }
}