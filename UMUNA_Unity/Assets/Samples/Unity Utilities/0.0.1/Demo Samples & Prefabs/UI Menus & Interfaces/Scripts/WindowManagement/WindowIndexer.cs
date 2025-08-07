using UnityEngine;
using UUP.ScriptableObjects.Data.Variables;

namespace UUP.UI.WindowManagement
{
    public class WindowIndexer : MonoBehaviour
    {
        [SerializeField] private int myWindowIndex;

        public int MyWindowIndex
        {
            get { return myWindowIndex; }
        }

        public IntVariableSO ActiveWindowIndex;

        private void Awake()
        {
            ActiveWindowIndex.SetValue(myWindowIndex);
        }
    }
}