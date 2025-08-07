using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UUP.ScriptableObjects.GameEvents;
using UUP.Utilities.GameObjectUtils;

namespace UUP.UI.WindowManagement
{
    public class UiWindowManager : MonoBehaviour
    {
        private GameObject _uiCanvas;

        public GameEvent OnUiOpen;
        public GameEvent OnUiClose;

        [SerializeField] private List<GameObject> uiWindowsList;

        private void Awake()
        {
            _uiCanvas = transform.GetChild(0).gameObject;
            ListUtils.SetActiveObjectFromList(uiWindowsList, 0);
        }

        private void Start()
        {
            _uiCanvas.SetActive(false);
        }

        public void ToggleUI()
        {
            if (_uiCanvas.activeSelf)
            {
                StartCoroutine(WaitForSecondsCoroutine(0.55f));
                OnUiClose.Raise();
            }

            else
            {
                _uiCanvas.SetActive(true);
                OnUiOpen.Raise();
            }
        }

        public void ChangeUiWindow(int data)
        {
            int nextWindowIndex = (int)data;

            if (nextWindowIndex > uiWindowsList.Count - 1)
            {
                nextWindowIndex = 0;
            }

            if (nextWindowIndex < 0)
            {
                nextWindowIndex = uiWindowsList.Count - 1;
            }
            
            ListUtils.SetActiveObjectFromList(uiWindowsList, nextWindowIndex);
        }

        private IEnumerator WaitForSecondsCoroutine(float secondsToWait)
        {
            yield return new WaitForSeconds(secondsToWait);

            // When timer finished
            _uiCanvas.SetActive(false);
        }
    }
}