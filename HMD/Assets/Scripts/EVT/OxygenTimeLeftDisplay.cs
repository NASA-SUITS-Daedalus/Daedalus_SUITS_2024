using UnityEngine;
using TMPro;

public class OxygenTimeLeftDisplay : MonoBehaviour
{
    public TELEMETRYDataHandler telemetryDataHandler;
    public TextMeshPro oxygenTimeLeftTextMeshPro;

    private void Update()
    {
        UpdateOxygenTimeLeft();
    }

    private void UpdateOxygenTimeLeft()
    {
        float oxygenTimeLeft = telemetryDataHandler.GetOxyTimeLeft("eva1");

        // Format the time as HH:MM:SS
        string formattedTime = FormatTime(oxygenTimeLeft);

        // Update the oxygen time left text
        oxygenTimeLeftTextMeshPro.text = formattedTime;
    }

    private string FormatTime(float timeInSeconds)
    {
        int hours = Mathf.FloorToInt(timeInSeconds / 3600);
        int minutes = Mathf.FloorToInt((timeInSeconds % 3600) / 60);
        int seconds = Mathf.FloorToInt(timeInSeconds % 60);

        return string.Format("{0:00}:{1:00}:{2:00}", hours, minutes, seconds);
    }
}