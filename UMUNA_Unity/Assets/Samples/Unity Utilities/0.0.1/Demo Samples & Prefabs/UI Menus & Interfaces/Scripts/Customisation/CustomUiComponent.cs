using UnityEngine;

namespace UUP.UI.Customisation
{
    public abstract class CustomUiComponent : MonoBehaviour
    {
        private void Awake()
        {
            Init();
        }
        private void OnValidate()
        {
            Init();
        }
     
        public abstract void Setup();
        public abstract void Configure();

        [ContextMenu("Init()")]
        public void Init()
        {
            Setup();
            Configure();
        }
    }
}