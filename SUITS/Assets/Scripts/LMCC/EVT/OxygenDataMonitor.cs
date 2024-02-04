using UnityEngine;
using TMPro;

public class OxygenMonitor : MonoBehaviour
{
    public TELEMETRYDataHandler TELEMETRYDataHandler;
    public TextMeshProUGUI oxyDataText;

    private string currentEVA = "eva1"; // Default to EVA1

    // Update is called once per frame
    void Update()
    {
        UpdateOxygenUI(currentEVA);
    }

    void UpdateOxygenUI(string currentEVA)
    {
        // Get oxygen data for the current EVA
        string oxyData = GetOxygenData(currentEVA);

        // Update the TextMeshPro text with oxygen telemetry data
        oxyDataText.text = $"Oxygen Data for {currentEVA}:\n{oxyData}";
    }

    string GetOxygenData(string currentEVA)
    {
        // Use your methods to retrieve oxygen data
        float oxyPriStorage = TELEMETRYDataHandler.GetOxyPriStorage(currentEVA);
        float oxySecStorage = TELEMETRYDataHandler.GetOxySecStorage(currentEVA);
        float oxyPriPressure = TELEMETRYDataHandler.GetOxyPriPressure(currentEVA);
        float oxySecPressure = TELEMETRYDataHandler.GetOxySecPressure(currentEVA);
        float oxyTimeLeft = TELEMETRYDataHandler.GetOxyTimeLeft(currentEVA);

        // Format the oxygen data as a string
        string oxyData = $"Oxy Pri Storage: {oxyPriStorage}\n" +
                         $"Oxy Sec Storage: {oxySecStorage}\n" +
                         $"Oxy Pri Pressure: {oxyPriPressure}\n" +
                         $"Oxy Sec Pressure: {oxySecPressure}\n" +
                         $"Oxy Time Left: {oxyTimeLeft} seconds";

        return oxyData;
    }

    // Public method to set the current EVA to EVA1
    public void SetCurrentEVA1()
    {
        currentEVA = "eva1";
        UpdateOxygenUI(currentEVA);
    }

    // Public method to set the current EVA to EVA2
    public void SetCurrentEVA2()
    {
        currentEVA = "eva2";
        UpdateOxygenUI(currentEVA);
    }
}
