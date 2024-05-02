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
        // Get all telemetry data as strings
        string allData = GetAllDataAsStrings();

        // Update the text displayed on the TextMeshProUGUI
        telemetryText.text = $"Telemetry Data for {currentEVA}:\n{allData}";
    }

    // Method to set the current EVA identifier
    public void SetCurrentEVA(string eva)
    {
        currentEVA = eva;
        UpdateTelemetryDataUI();
    }

    // Method to retrieve all telemetry data as strings
    string GetAllDataAsStrings()
    {
        // Retrieve telemetry data using TELEMETRYDataHandler
        float battTimeLeft = TELEMETRYDataHandler.GetBattTimeLeft(currentEVA);
        float oxyPriStorage = TELEMETRYDataHandler.GetOxyPriStorage(currentEVA);
        float oxySecStorage = TELEMETRYDataHandler.GetOxySecStorage(currentEVA);
        float oxyPriPressure = TELEMETRYDataHandler.GetOxyPriPressure(currentEVA);
        float oxySecPressure = TELEMETRYDataHandler.GetOxySecPressure(currentEVA);
        float oxyTimeLeft = TELEMETRYDataHandler.GetOxyTimeLeft(currentEVA);
        float heartRate = TELEMETRYDataHandler.GetHeartRate(currentEVA);
        float oxyConsumption = TELEMETRYDataHandler.GetOxyConsumption(currentEVA);
        float co2Production = TELEMETRYDataHandler.GetCO2Production(currentEVA);
        float suitPressureOxy = TELEMETRYDataHandler.GetSuitPressureOxy(currentEVA);
        float suitPressureCO2 = TELEMETRYDataHandler.GetSuitPressureCO2(currentEVA);
        float suitPressureOther = TELEMETRYDataHandler.GetSuitPressureOther(currentEVA);
        float suitPressureTotal = TELEMETRYDataHandler.GetSuitPresssureTotal(currentEVA);
        float fanPriRpm = TELEMETRYDataHandler.GetFanPriRpm(currentEVA);
        float fanSecRpm = TELEMETRYDataHandler.GetFanSecRpm(currentEVA);
        float helmetPressure = TELEMETRYDataHandler.GetHelmetPressure(currentEVA);
        float scrubberACO2Storage = TELEMETRYDataHandler.GetScrubber_A_CO2Storage(currentEVA);
        float scrubberBCO2Storage = TELEMETRYDataHandler.GetScrubber_B_CO2Storage(currentEVA);
        float temperature = TELEMETRYDataHandler.GetTemparature(currentEVA);
        float coolantMl = TELEMETRYDataHandler.GetCoolantMl(currentEVA);
        float coolantGasPressure = TELEMETRYDataHandler.GetCoolantGasPressure(currentEVA);
        float coolantLiquidPressure = TELEMETRYDataHandler.GetCoolantLiquidPressure(currentEVA);

        // Format telemetry data into a string
        return $"Battery Time Left: {battTimeLeft}\n" +
               $"Oxygen Primary Storage: {oxyPriStorage}\n" +
               $"Oxygen Secondary Storage: {oxySecStorage}\n" +
               $"Oxygen Primary Pressure: {oxyPriPressure}\n" +
               $"Oxygen Secondary Pressure: {oxySecPressure}\n" +
               $"Oxygen Time Left: {oxyTimeLeft}\n" +
               $"Heart Rate: {heartRate}\n" +
               $"Oxygen Consumption: {oxyConsumption}\n" +
               $"CO2 Production: {co2Production}\n" +
               $"Suit Pressure Oxygen: {suitPressureOxy}\n" +
               $"Suit Pressure CO2: {suitPressureCO2}\n" +
               $"Suit Pressure Other: {suitPressureOther}\n" +
               $"Suit Pressure Total: {suitPressureTotal}\n" +
               $"Fan Primary RPM: {fanPriRpm}\n" +
               $"Fan Secondary RPM: {fanSecRpm}\n" +
               $"Helmet Pressure: {helmetPressure}\n" +
               $"Scrubber A CO2 Storage: {scrubberACO2Storage}\n" +
               $"Scrubber B CO2 Storage: {scrubberBCO2Storage}\n" +
               $"Temperature: {temperature}\n" +
               $"Coolant mL: {coolantMl}\n" +
               $"Coolant Gas Pressure: {coolantGasPressure}\n" +
               $"Coolant Liquid Pressure: {coolantLiquidPressure}";
    }
}