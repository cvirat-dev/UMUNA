using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CustomUi
{
    public class GlobalServiceLocator : MonoBehaviour
    {
        // Main-Singleton
        public static GlobalServiceLocator main { get; private set; }

        // All die unten auskommentierten Zeilen
        // zeigen Umgang mit Child-Components von GlobalServiceLocator

        //public InputKeyManager InputKeyManager { get; private set; }
        //public SceneTransitionManager SceneTransitionManager { get; private set; }
        //public GameInventory GameInventory { get; private set; }

        private void Awake()
        {
            if (main != null)
            {
                Destroy(this.gameObject);
                return;
            }

            main = this;
            DontDestroyOnLoad(this.gameObject);

            //InputKeyManager = GetComponentInChildren<InputKeyManager>();
            //if (InputKeyManager != null) { print("InputKeyManager found!"); }

            //SceneTransitionManager = GetComponentInChildren<SceneTransitionManager>();
            //if (SceneTransitionManager != null) { print("SceneTransitionManager found!"); }

            //GameInventory = GetComponentInChildren<GameInventory>();
            //if (GameInventory != null) { print("GameInventory found!"); }
        }
    }
}