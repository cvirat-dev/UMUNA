using UnityEngine;

namespace UUP.Misc
{
    public class WobbleEffect : MonoBehaviour
    {
        private float wobbleDuration = 0.5f;   // Duration of the wobble effect in seconds
        private float wobbleIntensity = 0.25f;  // Intensity of the wobble

        private Vector3 initialScale;         // Store the initial scale of the GameObject
        private float wobbleTimer;            // Timer for the wobble effect
        private bool isWobbling = false;      // Flag to check if wobble is active

        private void Start()
        {
            initialScale = transform.localScale; // Store the initial scale
        }

        private void Update()
        {
            // Check if the wobble is active
            if (isWobbling)
            {
                // Check if the wobble timer is still running
                if (wobbleTimer < wobbleDuration)
                {
                    // Calculate the wobble scale based on a sine wave
                    float wobbleScale = Mathf.Sin(Time.time * Mathf.PI * (1 / wobbleDuration)) * wobbleIntensity;

                    // Apply the wobble effect to the GameObject's scale
                    transform.localScale = initialScale + new Vector3(wobbleScale, wobbleScale, wobbleScale);

                    // Update the timer
                    wobbleTimer += Time.deltaTime;
                }
                else
                {
                    // Reset the scale to the initial scale when the wobble effect is done
                    transform.localScale = initialScale;
                    isWobbling = false; // Deactivate the wobble
                }
            }
        }

        // Public method to start the wobble effect
        private void OnEnable()
        {
            isWobbling = true; // Activate the wobble
            wobbleTimer = 0f; // Reset the timer
        }
    }
}