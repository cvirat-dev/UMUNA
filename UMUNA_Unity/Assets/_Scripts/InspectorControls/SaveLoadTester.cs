using Umuna.Core.SharedData;
using UMUNA.AppManagement;
using UnityEngine;
using UUP.CustomAttributes;
using UUP.CustomAttributes.CustomTargets;
using VContainer;

namespace UMUNA.InspectorControls
{
    public class SaveLoadTester : MonoBehaviourT
    {
        [Inject]
        private AppManager appManager;

        [InspectorButton]
        public void Save() => appManager.SaveLoadSystem.Save();

        [InspectorButton]
        public void SaveAsync() => appManager.SaveLoadSystem.SaveAsync();

        [InspectorButton]
        public void Load() => appManager.SaveLoadSystem.Load();

        [InspectorButton]
        public void OpenInExplorer() => appManager.SaveLoadSystem.UmunaDataService.OpenInExplorer();
    }
}
