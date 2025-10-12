using Umuna.Core.SharedData;
using UMUNA.Bindings;
using UMUNA.EventManagement;

namespace UMUNA
{
    public class UmunaDataBinder : BinderBase<UmunaData>
    {
        public override void Bind(UmunaData data)
        {
            _data = data;
            EventManager.SaveLoad.UmunaData.OnUmunaDataLoaded.Invoke(data);
        }
    }
}
