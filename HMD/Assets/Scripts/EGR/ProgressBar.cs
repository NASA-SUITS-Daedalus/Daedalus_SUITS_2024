using UnityEngine;

public class ProgressBar : MonoBehaviour
{
    // Reference to the foreground object representing the progress
    public Transform foreground;

    // Define the min and max scale of the foreground object
    public float minScale = 0f;
    public float maxScale = 1f;

    // Update the progress bar based on the input value
    public void UpdateProgress(float value)
    {
        // Clamp the input value between minScale and maxScale
        float clampedValue = Mathf.Clamp(value, minScale, maxScale);

        // Calculate the new scale of the foreground object
        float newScale = Mathf.Lerp(minScale, maxScale, clampedValue);

        // Update the scale of the foreground object
        Vector3 scale = foreground.localScale;
        scale.x = newScale;
        foreground.localScale = scale;
    }
}
