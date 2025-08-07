using Umuna.Core.Data.Umuna;
using UMUNA.ScriptableObjects;
using UnityEngine;

namespace UMUNA.Bindings
{
    public class CameraDataBinder : BinderBase<CameraData>
    {
        [SerializeField]
        CameraDataSO cameraDataSO;

        public override void Bind(CameraData CameraData)
        {
            _data = CameraData;
            cameraDataSO.CameraData = CameraData;
        }
    }
}
