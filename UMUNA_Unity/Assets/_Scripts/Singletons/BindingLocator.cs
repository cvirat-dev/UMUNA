using UMUNA.Bindings;
using UnityEngine;
using UUP.Persistence;

namespace UMUNA.Singletons
{
    public class BindingLocator : PersistentSingleton<BindingLocator>
    {
        public CameraDataBinder CameraDataBinder { get; private set; }
        public ExportNotesBinder ExportNotesBinder { get; private set; }

        protected override void Awake()
        {
            base.Awake();
            CameraDataBinder = FindInChildren<CameraDataBinder>();
            ExportNotesBinder = FindInChildren<ExportNotesBinder>();
        }

        private TComp FindInChildren<TComp>() where TComp : MonoBehaviour
        {
            var comp = GetComponentInChildren<TComp>();
            return comp ?? throw new System.Exception($"No {nameof(TComp)} found in children of {name}");
        }

        private bool FindInChildren<TComp>(out TComp comp) where TComp : MonoBehaviour
        {
            comp = GetComponentInChildren<TComp>();
            return comp != null;
        }
    }
}
