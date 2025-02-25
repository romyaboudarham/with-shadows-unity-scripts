using UnityEngine;

/*
 *  Fade Material Out
 *  -----------------------------
 *  Changes the transparency of the object to 0 over time
 *  1. Object material must have a shader that supports transparency
*/
public class PhotoFadeOut : MonoBehaviour
{
    public float fadeDuration = 1.0f; // Time for the fade-out effect
    public Renderer objectRenderer;  // Assign the Renderer component in the Inspector
    private Material objectMaterial; // The material instance for this object
    private float fadeProgress = 0.0f;

    private void Start()
    {
        // Ensure the Renderer is assigned
        if (objectRenderer == null)
        {
            objectRenderer = GetComponent<Renderer>();
        }

        // Create a unique material instance for this object
        if (objectRenderer != null)
        {
            objectMaterial = objectRenderer.material; // This creates an instance of the material
        }

        if (objectMaterial == null)
        {
            Debug.LogError("No material found on this object! Fading won't work.");
            enabled = false; // Disable the script if material is missing
            return;
        }
    }

    private void Update()
    {
        if (fadeDuration <= 0)
        {
            Debug.LogError("Fade duration must be greater than 0. Defaulting to 1 second.");
            fadeDuration = 1.0f; // Prevent division by zero
        }

        // Increment fade progress based on fadeDuration
        fadeProgress += Time.deltaTime / fadeDuration;

        // Adjust material transparency
        if (objectMaterial != null)
        {
            Color currentColor = objectMaterial.color;
            currentColor.a = Mathf.Lerp(1.0f, 0.0f, fadeProgress); // Fade alpha
            objectMaterial.color = currentColor;
        }

        // Once fade is complete, disable the object
        if (fadeProgress >= 1.0f)
        {
            gameObject.SetActive(false); // Hide the object
            Debug.Log("Object faded out and disabled.");
        }
    }
}
