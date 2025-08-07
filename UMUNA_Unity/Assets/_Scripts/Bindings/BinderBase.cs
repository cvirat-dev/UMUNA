using UMUNA.AppManagement;
using UMUNA.SavingSystem;
using UnityEngine;
using UUP.CustomDataTypes.Serializables;

namespace UMUNA.Bindings
{
    public abstract class BinderBase<TData> : MonoBehaviour, IBind<TData>
    {
        [field: SerializeField]
        public GuidSRZ Id { get; set; } = GuidSRZ.NewGuid();
        
        [SerializeField]
        protected TData _data;

        protected AppManager appManager;
        
        public abstract void Bind(TData data);
    }
}
