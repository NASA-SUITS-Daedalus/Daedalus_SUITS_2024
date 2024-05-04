using UnityEngine;
using TMPro;

public class PrimaryOxygenLevelDisplay : MonoBehaviour
{
    public TELEMETRYDataHandler telemetryDataHandler;
    public DataRanges dataRanges;
    public Transform background;
    public Transform foreground;
    public TextMeshPro oxygenLevelTextMeshPro;
    public Color normalColor = Color.green;
    public Color warningColor = Color.yellow;
    public Color criticalColor = Color.red;

    // This script allows us to get current EVA Number
    public EVANumberHandler evaNumberHandler;

    private void Update()
    {
        UpdateOxygenLevel();
    }

    private void UpdateOxygenLevel()
    {
        int evaNumber = evaNumberHandler.getEVANumber();
        string evaKey = $"eva{evaNumber}";

        float oxygenLevel = telemetryDataHandler.GetOxyPriStorage(evaKey);

        // Calculate the progress based on the oxygen level and data ranges
        float progress = Mathf.Clamp01((oxygenLevel - (float)dataRanges.oxy_pri_storage.Min) / (float)(dataRanges.oxy_pri_storage.Max - dataRanges.oxy_pri_storage.Min));

        // Update the foreground bar size
        UpdateForegroundBarSize(progress);

        // Update the oxygen level text
        oxygenLevelTextMeshPro.text = $"{oxygenLevel:F0}%";

        // Change the color of the bar based on the oxygen level
        if (oxygenLevel < 30f)
        {
            foreground.GetComponent<Renderer>().material.color = criticalColor;
        }
        else if (oxygenLevel < 60f)
        {
            foreground.GetComponent<Renderer>().material.color = warningColor;
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