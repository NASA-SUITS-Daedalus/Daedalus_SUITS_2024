using UnityEngine;
using TMPro;

public class OxygenMonitor : MonoBehaviour
{
    public TELEMETRYDataHandler TELEMETRYDataHandler;
    public TextMeshProUGUI oxyPriStorageText;
    public TextMeshProUGUI oxySecStorageText;
    public TextMeshProUGUI oxyPriPressureText;
    public TextMeshProUGUI oxySecPressureText;
    public TextMeshProUGUI oxyTimeLeftText;

    private string currentEVA = "eva1"; // Default to EVA1

    // Update is called once per frame
    void Update()
    {
        // Get the telemetry data based on the current EVA
        EvaData evaData = TELEMETRYDataHandler.GetTelemetryData(currentEVA);

        // Update UI text for oxygen telemetry data
        UpdateOxygenUI(evaData);
    }

    void UpdateOxygenUI(EvaData evaData)
    {
        // Update the TextMeshPro texts with oxygen telemetry data
        oxyPriStorageText.text = $"Oxy Pri Storage: {evaData.oxy_pri_storage}";
        oxySecStorageText.text = $"Oxy Sec Storage: {evaData.oxy_sec_storage}";
        oxyPriPressureText.text = $"Oxy Pri Pressure: {evaData.oxy_pri_pressure}";
        oxySecPressureText.text = $"Oxy Sec Pressure: {evaData.oxy_sec_pressure}";
        oxyTimeLeftText.text = $"Oxy Time Left: {evaData.oxy_time_left} seconds";
    }

    // Public method to set the current EVA to EVA1
    public void SetCurrentEVA1()
    {
        currentEVA = "eva1";
    }

    // Public method to set the current EVA to EVA2
    public void SetCurrentEVA2()
    {
        currentEVA = "eva2";
    }
}
