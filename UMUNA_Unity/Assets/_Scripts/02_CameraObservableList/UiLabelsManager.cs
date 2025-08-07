using UUP.ScriptableObjects.Data.Variables;
using UnityEngine;
using UMUNA.ScriptableObjects;

namespace UMUNA._Scripts.CameraObservableList
{
    public class UiLabelsManager : MonoBehaviour
    {
        [SerializeField] StringVariableSO currentPositionIndexMssg;
        [SerializeField] StringVariableSO numberOfDefinedPositionsMssg;
        [SerializeField] CameraDataSO cameraDataSO;

        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
            // One time only: manually set the current location text to the first index of the camera positions record
            OnRegisterChange();
            UpdateCurrentPositionMssg(-1);
        }

        private void OnEnable()
        {
            cameraDataSO.OnCameraDataChanged += OnRegisterChange;
        }

        private void OnDisable()
        {
            cameraDataSO.OnCameraDataChanged -= OnRegisterChange;
        }

        private void OnRegisterChange()
        {
            numberOfDefinedPositionsMssg.Value = $"{cameraDataSO.Count} positions defined";
            UpdateCurrentPositionMssg(cameraDataSO.CurrentCameraIndex);
        }

        private void UpdateCurrentPositionMssg(int index)
        {
            var uiIndex = index + 1;

            if (uiIndex == 0)
            {
                currentPositionIndexMssg.Value = "No Position currently set";
                return;
            }
            else
            {
                currentPositionIndexMssg.Value = $"Current Position Index: {uiIndex}";
            }

        }
    }
}