using UnityEngine;
using TMPro;

public class OxygenLevelDisplay : MonoBehaviour
{
    public TELEMETRYDataHandler telemetryDataHandler;
    public DataRanges dataRanges;
    public Transform background;
    public Transform foreground;
    public TextMeshPro oxygenLevelTextMeshPro;
    public Color normalColor = Color.green;
    public Color criticalColor = Color.red;

    private void Update()
    {
        UpdateOxygenLevel();
    }

    private void UpdateOxygenLevel()
    {
        float oxygenLevel = telemetryDataHandler.GetOxyPriStorage("eva1");

        // Calculate the progress based on the oxygen level and data ranges
        float progress = Mathf.Clamp01((oxygenLevel - (float)dataRanges.oxy_pri_storage.Min) / (float)(dataRanges.oxy_pri_storage.Max - dataRanges.oxy_pri_storage.Min));

        // Update the foreground bar size
        UpdateForegroundBarSize(progress);

        // Update the oxygen level text
        oxygenLevelTextMeshPro.text = $"{oxygenLevel:F0}%";

        // Change the color of the bar if it enters the critical zone
        if (oxygenLevel < 50f)
        {
            foreground.GetComponent<Renderer>().material.color = criticalColor;
        }
        else
        {
            foreground.GetComponent<Renderer>().material.color = normalColor;
        }
    }

    private void UpdateForegroundBarSize(float progress)
    {
        Vector3 backgroundScale = background.localScale;
        Vector3 foregroundScale = foreground.localScale;

        // Calculate the new scale of the foreground object
        foregroundScale.x = backgroundScale.x * progress;

        // Update the scale of the foreground object
        foreground.localScale = foregroundScale;

        // Align the left side of the foreground bar with the left side of the background bar
        Vector3 foregroundPosition = foreground.localPosition;
        Vector3 backgroundPosition = background.localPosition;
        float leftPivot = backgroundPosition.x - backgroundScale.x * 0.5f;

        foregroundPosition.x = leftPivot + foregroundScale.x * 0.5f;

        // Update foreground local position
        foreground.localPosition = foregroundPosition;
    }
}