using UUP.ScriptableObjects.Data.Variables;
using UnityEngine;
using Umuna.Core.Data.Umuna;
using UMUNA.EventManagement;
using Umuna.Core.Data;

namespace UMUNA._Scripts.CameraObservableList
{
    public class UiLabelsManager : MonoBehaviour
    {
        CameraData _cameraData = new();
        [SerializeField] StringVariableSO currentPositionIndexMssg;
        [SerializeField] StringVariableSO numberOfDefinedPositionsMssg;

        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
            // One time only: manually set the current location text to the first index of the camera positions record
            OnRegisterChange();
            UpdateCurrentPositionMssg(-1);
        }

        private void OnEnable()
        {
            EventManager.SaveLoad.UmunaData.OnUmunaDataLoaded.AddListener(Bind);
        }

        private void Bind(UmunaData arg0)
        {
            _cameraData = arg0.CameraData;
            OnRegisterChange();
        }

        private void OnDisable()
        {
            EventManager.SaveLoad.UmunaData.OnUmunaDataLoaded.RemoveListener(Bind);
        }

        private void OnRegisterChange()
        {
            numberOfDefinedPositionsMssg.Value = $"{_cameraData.SavedPositions.Count} positions defined";
            UpdateCurrentPositionMssg(_cameraData.CurrentCameraIndex);
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