using System.Collections;
using UnityEngine;
using UUP.ScriptableObjects.Data.Variables;

namespace UUP.UI.Effects
{
    public class UiWobbleEffect : MonoBehaviour
    {
        private float wobbleIntensity = 0.1f;

        public FloatVariableSO WobbleDuration;

        private Vector3 originalScale;

        private void Start()
        {
            originalScale = transform.localScale;
        }

        public void StartWobble()
        {
            StartCoroutine(WobbleCoroutine());
        }

        private IEnumerator WobbleCoroutine()
        {
            float elapsedTime = 0f;
            while (elapsedTime < WobbleDuration.Value)
            {
                // Calculate the wobble scale based on a half sinus function
                float wobbleScale = 1 + Mathf.Sin(elapsedTime / WobbleDuration.Value * Mathf.PI) * wobbleIntensity;
                transform.localScale = originalScale * wobbleScale;

                elapsedTime += Time.deltaTime;
                yield return null;
            }

            // Ensure the scale is back to its original size
            transform.localScale = originalScale;
        }
    }
}