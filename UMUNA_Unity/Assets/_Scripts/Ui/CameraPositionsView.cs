using System.Collections;
using UMUNA.ScriptableObjects;
using UnityEngine;
using UnityEngine.UIElements;

namespace UMUNA.Ui
{
    public abstract class CameraPositionsView : MonoBehaviour
    {
        [SerializeField] CameraDataSO cameraDataSO;

        [SerializeField] protected UIDocument uiDocument;
        [SerializeField] protected StyleSheet styleSheet;

        protected VisualElement root;
        protected VisualElement cameraPositionsContainer;


        IEnumerator Start()
        {
            yield return StartCoroutine(InitializeView());
        }

        public abstract IEnumerator InitializeView(int size = 20);
    }
}
