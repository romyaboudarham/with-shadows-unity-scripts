using UnityEngine;
using UnityEngine.Rendering;

/*
 *  Post Processing FadeIn
 *  -----------------------------
 *  Increases the intensity of the post processing effect over time
*/
public class PostProcessingFadeIn : MonoBehaviour
{
    public Volume postProcessingVolume; // Assign in the inspector
    public float fadeDuration = 2f; // Duration in seconds for the fade-in
    public float postProcessingStartingWeight = 0f; // Starting weight
    public float postProcessingEndingWeight = 1f; // Ending weight
    private float elapsedTime = 0f; // Tracks time elapsed

    void Update()
    {
        if (postProcessingVolume != null)
        {
            // Increment elapsed time
            elapsedTime += Time.deltaTime;

            // Calculate the normalized time (0 to 1)
            float t = Mathf.Clamp01(elapsedTime / fadeDuration);

            // Interpolate the weight between starting and ending values
            float postProcessingWeight = Mathf.Lerp(postProcessingStartingWeight, postProcessingEndingWeight, t);

            // Apply the weight to the post-processing volume
            postProcessingVolume.weight = postProcessingWeight;

            Debug.Log($"Post-processing weight: {postProcessingWeight}");
        }
    }
}
