using UnityEngine;
using UUP.ScriptableObjects.GameEvents.Serialized;

namespace UUP.UI.Events
{
    /// <summary>
    /// This class allows to quickly raise an ScriptableObjectArchitecture.IntGameEvent directly from extern
    /// For example: Using the CustomButton-Prefab where the IntEventShooter can be attached and controlled by an UnityEvent (OnButtonClick)
    /// </summary>
    public class IntEventShooter : MonoBehaviour
    {
        public IntGameEvent IntGameEvent;

        public void ShootIntGameEvent(int index)
        {
            IntGameEvent.Raise(index);
        }
    }
}