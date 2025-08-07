using System;
using UMUNA.Data;
using UMUNA.EventManagement;
using UnityEngine;
using UUP.CustomAttributes;

namespace UMUNA.Bindings
{
    public class ExportNotesBinder : BinderBase<ExportNotes>
    {
        public override void Bind(ExportNotes data)
        {
            base._data = data;
            Debug.Log("ExportNotesBinder: Binding completed");
        }

        private void OnEnable()
        {
            EventManager.SaveLoad.OnBeforeSave.AddListener(OnSave);
        }

        private void OnDisable()
        {
            EventManager.SaveLoad.OnBeforeSave.RemoveListener(OnSave);
        }

        [InspectorButton]
        private void OnSave()
        {
            _data.ExportDate = DateTime.Now;
            _data.NumberOfExports++;
            //data.NumberOfCameraPositions = ServiceLocator.Instance.AppManager.Inventory.CameraRegisterData.CameraPositions.Count;
        }
    }
}
