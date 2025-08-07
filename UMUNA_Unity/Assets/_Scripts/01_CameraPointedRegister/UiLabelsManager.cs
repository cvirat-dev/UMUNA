using UUP.CustomAttributes;
using UUP.ScriptableObjects.Data.Entries.NotifiedRegisters;
using UUP.ScriptableObjects.Data.Variables;
using UnityEngine;

namespace UMUNA._Scripts.CameraPositionNotifRegister

{
    /// <summary>
    /// This class manages the state of the camera positions record and updates the UI text accordingly
    /// </summary>
    public class UiLabelsManager : MonoBehaviour
    {
        [SerializeField]
        private StringVariableSO currentLocationText;

        [SerializeField]
        private StringVariableSO spatialRecordStateText;

        [SerializeField]
        private SpatialOrientationNotifRegister cameraPositionsRecord;

        [SerializeField]
        private IntVariableSO currentCameraPosition;

        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
            // One time only: manually set the current location text to the first index of the camera positions record
            spatialRecordStateText.Value = $"{cameraPositionsRecord.NumberOfDefinedEntries} / {cameraPositionsRecord.Length} defined";
            OnCameraPositionChange(-1);
        }

        private void OnEnable()
        {
            cameraPositionsRecord.OnAnyChange += OnRegisterChange;
            currentCameraPosition.OnValueSet += OnCameraPositionChange;
        }

        private void OnDisable()
        {
            cameraPositionsRecord.OnAnyChange -= OnRegisterChange;
            currentCameraPosition.OnValueSet -= OnCameraPositionChange;
        }

        private void OnRegisterChange()
        {
            var numberOfSetEntries = cameraPositionsRecord.NumberOfDefinedEntries;
            string uiText = $"{numberOfSetEntries} / {cameraPositionsRecord.Length} defined";
            spatialRecordStateText.Value = uiText;
        }

        private void OnCameraPositionChange(int index)
        {
            var uiIndex = index + 1;

            if (uiIndex == 0)
            {
                currentLocationText.Value = "No Position currently set";
                return;
            }
            else
            {
                currentLocationText.Value = $"Current Position Index: {uiIndex}";
            }

        }

        [InspectorButton]
        public void CheckIfRegisterChanged()
        {
            OnRegisterChange();
        }
    }
}