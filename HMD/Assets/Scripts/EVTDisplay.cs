using UnityEngine;
using TMPro;

public class EVTDisplay : MonoBehaviour
{
    // Reference to the TELEMETRYDataHandler script
    public TELEMETRYDataHandler TELEMETRYDataHandler;

    // Reference to the TextMeshProUGUI component to display telemetry data
    public TextMeshPro telemetryText;

    // The current EVA identifier
    private string currentEVA = "eva1";

    void Start()
    {
        // Set the current EVA identifier to "eva1" right away
        currentEVA = "eva1";

        // Call the method to update telemetry data when the script starts
        UpdateTelemetryDataUI();
    }

    void Update()
    {
        // Optionally, update telemetry data continuously in Update method
        UpdateTelemetryDataUI();
    }

    // Method to update telemetry data displayed on the TextMeshProUGUI
    public void UpdateTelemetryDataUI()
    {
        // Get important telemetry data
        string importantData = GetImportantData();

        // Update the text displayed on the TextMeshProUGUI
        telemetryText.text = $"Telemetry Data for {currentEVA}:\n{importantData}";
    }

    // Method to set the current EVA identifier
    public void SetCurrentEVA(string eva)
    {
        currentEVA = eva;
        UpdateTelemetryDataUI();
    }

    // Method to retrieve important telemetry data
    string GetImportantData()
    {
        // Retrieve telemetry data using TELEMETRYDataHandler
        float battTimeLeft = TELEMETRYDataHandler.GetBattTimeLeft(currentEVA);
        float heartRate = TELEMETRYDataHandler.GetHeartRate(currentEVA);
        float oxyConsumption = TELEMETRYDataHandler.GetOxyConsumption(currentEVA);
        float co2Production = TELEMETRYDataHandler.GetCO2Production(currentEVA);

        // Format telemetry data into a string
        return $"Battery Time Left: {battTimeLeft}\n" +
               $"Heart Rate: {heartRate}\n" +
               $"Oxygen Consumption: {oxyConsumption}\n" +
               $"CO2 Production: {co2Production}\n";
    }
}
