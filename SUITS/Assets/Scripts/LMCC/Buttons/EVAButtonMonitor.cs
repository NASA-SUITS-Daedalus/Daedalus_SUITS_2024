using UnityEngine;
using TMPro;

public class TelemetryDisplayController : MonoBehaviour
{
    public TELEMETRYDataHandler TELEMETRYDataHandler;
    public TextMeshProUGUI dataDisplayText;

    // Update is called once per frame
    void Update()
    {
        // Check for button press or user input to display data
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            DisplayEVAData("eva1");
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            DisplayEVAData("eva2");
        }
    }

    void DisplayEVAData(string eva)
    {
        // Get the telemetry data for the specified EVA
        EvaData evaData = TELEMETRYDataHandler.GetTelemetryData(eva);

        // Display all telemetry data for the specified EVA
        dataDisplayText.text = $"Telemetry Data for {eva}:\n" +
            $"Batt Time Left: {evaData.batt_time_left}\n" +
            $"Oxy Pri Storage: {evaData.oxy_pri_storage}\n" +
            $"Oxy Sec Storage: {evaData.oxy_sec_storage}\n" +
            $"Oxy Pri Pressure: {evaData.oxy_pri_pressure}\n" +
            $"Oxy Sec Pressure: {evaData.oxy_sec_pressure}\n" +
            $"Oxy Time Left: {evaData.oxy_time_left}\n" +
            $"Heart Rate: {evaData.heart_rate}\n" +
            // Add more properties as needed
            $"Coolant Liquid Pressure: {evaData.coolant_liquid_pressure}\n";

        // Additional logic can be added to clear the display after a certain duration
    }
}
