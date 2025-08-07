using System.Collections;
using UnityEngine;
using UUP.ScriptableObjects.Data.Variables;
using UUP.ScriptableObjects.GameEvents.Serialized;

namespace UUP.UI.WindowManagement
{
    public class UiWindowIndexSwitcher : MonoBehaviour
    {
        public IntVariableSO WindowIndex;
        public IntVariableSO NextWindowIndex;
        public FloatVariableSO WindowChangeDuration;
        public IntGameEvent WindowChangedGE;

        private bool _timerIsRunning;

        public void SwitchToNextWindow()
        {
            if (!_timerIsRunning)
            {
                NextWindowIndex.SetValue(WindowIndex.Value + 1);
                StartCoroutine(WaitForSecondsCoroutine(WindowChangeDuration.Value));
            }
        }

        public void SwitchToPreviousWindow()
        {
            if (!_timerIsRunning)
            {
                NextWindowIndex.SetValue(WindowIndex.Value - 1);
                StartCoroutine(WaitForSecondsCoroutine(WindowChangeDuration.Value));
            }
        }

        private IEnumerator WaitForSecondsCoroutine(float secondsToWait)
        {
            Debug.Log("wait for: " + secondsToWait + " seconds");
            _timerIsRunning = true;
            yield return new WaitForSeconds(secondsToWait);

            // When timer finished
            WindowChangedGE.Raise(NextWindowIndex.Value);
            _timerIsRunning = false;
        }
    }
}