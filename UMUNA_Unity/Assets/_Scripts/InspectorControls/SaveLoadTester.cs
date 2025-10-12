using Umuna.Core.SharedData;
using UMUNA.Data;
using UnityEngine;
using UUP.CustomAttributes;
using UUP.CustomAttributes.CustomTargets;

namespace UMUNA.InspectorControls
{
    public class SaveLoadTester : MonoBehaviourT
    {
        [InspectorButton]
        public void Save()
        {
            ServiceLocator.Instance.AppManager.SaveLoadSystem.Save();
        }

        [InspectorButton]
        public void SaveAsync()
        {
            ServiceLocator.Instance.AppManager.SaveLoadSystem.SaveAsync();
        }

        [InspectorButton]
        public void Load()
        {
            ServiceLocator.Instance.AppManager.SaveLoadSystem.Load();
        }

        [InspectorButton]
        public void DebugInventory()
        {
            UmunaData umunData = ServiceLocator.Instance.AppManager.UmunaData;
            string currentData = ServiceLocator.Instance.AppManager.SaveLoadSystem.UmunaDataService.GetSerializedData(umunData);
            Debug.Log(currentData);
        }

        [InspectorButton]
        public void OpenInExplorer()
        {
            ServiceLocator.Instance.AppManager.SaveLoadSystem.UmunaDataService.OpenInExplorer();
        }
    }
}
