using System;
using Umuna.Core.SharedData.Umuna;
using UMUNA.EventManagement;
using UnityEngine;
using UnityEngine.Events;
using UUP.Components.CameraSystem;
using UUP.ScriptableObjects.Data.Variables;

namespace UMUNA.CameraSystem
{
    //public class CameraSystemController : MonoBehaviour
    //{
    //    [SerializeField] SceneLikeRbCameraController sceneLikeRbCameraController;
    //    [SerializeField] IntVariableSO currentCameraPosition;

    //    private void OnEnable()
    //    {
    //        sceneLikeRbCameraController.OnCameraMovement += EventManager.CameraSystem.OnNotifyCameraMovement.Invoke;
    //        sceneLikeRbCameraController.OnCameraReset += EventManager.CameraSystem.OnNotifyCameraReset.Invoke;
    //        EventManager.CameraSystem.OnNotifyCameraMovement.AddListener(ResetCurrentCameraIndex());
    //        EventManager.Ui.OnAddNewCameraPosition.AddListener(AddNewCameraPosition);
    //        EventManager.Ui.OnEmptyList.AddListener(EmptyPositionList);
    //        EventManager.Ui.OnNextCameraPosition.AddListener(NextCameraPosition);
    //        EventManager.Ui.OnResetDefaultCameraPosition.AddListener(ResetDefaultCameraPosition);
    //        EventManager.Ui.OnPositionSelected.AddListener(SetCameraPositionAt);

    //    }

    //    private void OnDisable()
    //    {
    //        sceneLikeRbCameraController.OnCameraMovement -= EventManager.CameraSystem.OnNotifyCameraMovement.Invoke;
    //        sceneLikeRbCameraController.OnCameraReset -= EventManager.CameraSystem.OnNotifyCameraReset.Invoke;
    //        EventManager.CameraSystem.OnNotifyCameraMovement.RemoveListener(ResetCurrentCameraIndex());
    //        EventManager.Ui.OnAddNewCameraPosition.RemoveListener(AddNewCameraPosition);
    //        EventManager.Ui.OnEmptyList.RemoveListener(EmptyPositionList);
    //        EventManager.Ui.OnNextCameraPosition.RemoveListener(NextCameraPosition);
    //        EventManager.Ui.OnResetDefaultCameraPosition.RemoveListener(ResetDefaultCameraPosition);
    //        EventManager.Ui.OnPositionSelected.RemoveListener(SetCameraPositionAt);
    //    }

    //    private UnityAction ResetCurrentCameraIndex()
    //    {
    //        return () => cameraData.CurrentCameraIndex = -1;
    //    }

    //    private void NextCameraPosition()
    //    {
    //        if (cameraData.Count == 0)
    //            return;

    //        cameraData.CurrentCameraIndex++;
    //        sceneLikeRbCameraController.MoveTo(cameraData.CurrentCameraPosition);
    //    }

    //    private void ResetDefaultCameraPosition()
    //    {
    //        sceneLikeRbCameraController.ResetCameraPosition();
    //        cameraData.CurrentCameraIndex = -1;
    //    }

    //    private void EmptyPositionList()
    //    {
    //        cameraData.ModifySavedPositions(ListOperation.Clear, null);
    //    }

    //    private void AddNewCameraPosition()
    //    {
    //        cameraData.ModifySavedPositions(ListOperation.Add, sceneLikeRbCameraController.CurrentPosition);
    //    }

    //    private void SetCameraPositionAt(int arg0)
    //    {
    //        if (arg0 < 0 || arg0 >= cameraData.Count)
    //            throw new IndexOutOfRangeException("Index out of range");

    //        cameraData.CurrentCameraIndex = arg0;
    //        sceneLikeRbCameraController.MoveTo(cameraData.CurrentCameraPosition);
    //    }
    //}
}
