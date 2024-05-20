using UnityEngine;
using TMPro;

public class OxygenMonitor : MonoBehaviour
{
    public TELEMETRYDataHandler TELEMETRYDataHandler;
    public TextMeshProUGUI oxyDataText;

    private string currentEVA = "eva1";
    private string currentSuitData = "Oxygen";

    void Update()
    {
        // Only update the UI if the "Oxygen" button is clicked
        if (currentSuitData == "Oxygen")
        {
            UpdateOxygenUI();
        }
    }

    public void UpdateOxygenUI()
    {
        string oxyData = GetOxygenData();
        oxyDataText.text = $"Oxygen Data for {currentEVA}:\n{oxyData}";
    }

    public void SetCurrentEVA(string eva)
    {
        currentEVA = eva;
        UpdateOxygenUI();
    }

    public void SetCurrentSuitData(string suitData)
    {
        currentSuitData = suitData;
        // Optionally clear the text when switching to another data type
        UpdateOxygenUI();
    }

    string GetOxygenData()
    {
        float oxyPriStorage = TELEMETRYDataHandler.GetOxyPriStorage(currentEVA);
        float oxySecStorage = TELEMETRYDataHandler.GetOxySecStorage(currentEVA);
        float oxyPriPressure = TELEMETRYDataHandler.GetOxyPriPressure(currentEVA);
        float oxySecPressure = TELEMETRYDataHandler.GetOxySecPressure(currentEVA);
        float oxyTimeLeft = TELEMETRYDataHandler.GetOxyTimeLeft(currentEVA);

        return $"Oxy Pri Storage: {oxyPriStorage}\n" +
               $"Oxy Sec Storage: {oxySecStorage}\n" +
               $"Oxy Pri Pressure: {oxyPriPressure}\n" +
               $"Oxy Sec Pressure: {oxySecPressure}\n" +
               $"Oxy Time Left: {oxyTimeLeft} seconds";
    }
}
