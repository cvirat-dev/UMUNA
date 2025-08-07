using UnityEngine;
using UnityEngine.Events;

namespace UMUNA.EventManagement
{
    public class EventParams
    {
        public string PropertyName;
        public object Value;
    }

    public static class EventManager
    {
        public static CameraPositionEvents CameraPosition = new();
        public static UiEvents Ui = new();
        public static CameraSystemEvents CameraSystem = new();
        public static SaveLoadEvents SaveLoad = new();

        public class CameraPositionEvents
        {
            public UnityEvent<int> OnNewCameraPositionAt = new();
        }

        public class UiEvents
        {
            public UnityEvent OnAddNewCameraPosition = new();
            public UnityEvent OnEmptyList = new();
            public UnityEvent OnResetDefaultCameraPosition = new();
            public UnityEvent OnNextCameraPosition = new();
            public UnityEvent<int> OnPositionSelected = new();
        }

        public class CameraSystemEvents
        {
            public UnityEvent OnNotifyCameraMovement = new();
            public UnityEvent OnNotifyCameraReset = new();
        }

        public class SaveLoadEvents
        {
            public UnityEvent OnBeforeSave = new();
            public UnityEvent OnAfterSave = new();
            public UnityEvent OnBeforeLoad = new();
            public UnityEvent OnAfterLoad = new();
            public UnityEvent OnBindingCompleted = new();
        }
    }
}