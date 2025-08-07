using System.Linq;
using UnityEngine;
using UUP.Persistence;

namespace UMUNA.Utils
{
    public class SceneHelper : PersistentSingleton<SceneHelper>
    {

        public T FindMonoBehaviour<T>() where T : MonoBehaviour
        {
            T obj = FindObjectsByType<T>(FindObjectsSortMode.None).FirstOrDefault();
            if (obj == null)
            {
                Debug.LogError($"No object of type {typeof(T).Name} found in the scene.");
            }
            return obj;
        }

        public T[] FindAllMonoBehaviours<T>() where T : MonoBehaviour
        {
            return FindObjectsByType<T>(FindObjectsSortMode.None);
        }
    }
}
