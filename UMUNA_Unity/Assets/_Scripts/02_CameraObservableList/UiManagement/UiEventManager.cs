using UMUNA.EventManagement;
using UnityEngine;
using UUP.CustomAttributes;
using UUP.EventManagement;
using UUP.ScriptableObjects.GameEvents;

namespace UMUNA
{
    public class UiEventManager : AutoEventSubscriber
    {
        [SerializeField] GameEvent OnAddNewCameraPosition;
        [SerializeField] GameEvent OnEmptyList;
        [SerializeField] GameEvent OnResetDefaultCameraPosition;
        [SerializeField] GameEvent OnNextCameraPosition;

        #region CameraListControls
        [AutoSubscribeToGameEvent(nameof(OnAddNewCameraPosition))]
        [InspectorButton]
        public void AddNewPosition()
        {
            EventManager.Ui.OnAddNewCameraPosition.Invoke();
        }

        [AutoSubscribeToGameEvent(nameof(OnEmptyList))]
        [InspectorButton]
        public void EmptyList()
        {
            EventManager.Ui.OnEmptyList.Invoke();
        }

        [AutoSubscribeToGameEvent(nameof(OnNextCameraPosition))]
        [InspectorButton]
        public void NextCameraPosition()
        {
            EventManager.Ui.OnNextCameraPosition.Invoke();
        }
        #endregion

        #region CameraReset
        [AutoSubscribeToGameEvent(nameof(OnResetDefaultCameraPosition))]
        [InspectorButton]
        public void ResetDefaultCameraPosition()
        {
            EventManager.Ui.OnResetDefaultCameraPosition.Invoke();
        }
        #endregion

        #region InfoElements

        #endregion


        #region ListView

        #endregion

    }
}
