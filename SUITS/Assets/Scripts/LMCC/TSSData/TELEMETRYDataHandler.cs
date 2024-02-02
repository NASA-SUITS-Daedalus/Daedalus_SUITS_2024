using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// IMPORTANT: The TelemetryWrapper class is used to handle the additional
// wrapper layer in the received JSON string. The JSON structure includes
// an outer layer named "telemetry," and the TelemetryWrapper helps to
// unwrap and deserialize the data within that layer.
[System.Serializable]
public class TelemetryWrapper
{
    // The telemetry field represents the inner data structure containing
    // the actual telemetry information we want to extract and use.
    public TelemetryData telemetry;
}


[System.Serializable]
public class TelemetryData
{
    public float eva_time;
    public EvaData eva1;
    public EvaData eva2;
}

[System.Serializable]
public class EvaData
{
    public float batt_time_left;
    public float oxy_pri_storage;
    public float oxy_sec_storage;
    public float oxy_pri_pressure;
    public float oxy_sec_pressure;
    public float oxy_time_left;
    public float heart_rate;
    public float oxy_consumption;
    public float co2_production;
    public float suit_pressure_oxy;
    public float suit_pressure_co2; // Note: Corrected typo in the JSON
    public float suit_pressure_other;
    public float suit_pressure_total;
    public float fan_pri_rpm;
    public float fan_sec_rpm;
    public float helmet_pressure_co2;
    public float scrubber_a_co2_storage;
    public float scrubber_b_co2_storage;
    public float temperature;
    public float coolant_ml;
    public float coolant_gas_pressure;
    public float coolant_liquid_pressure;
}

public class TELEMETRYDataHandler : MonoBehaviour
{
    // Reference to the TSSc Data Handler script
    public TSScDataHandler Data;

    public TelemetryData telemetryData;

    // Start is called before the first frame update
    void Start()
    {
        // Initialize the telemetry data
        telemetryData = new TelemetryData();
    }

    // Update is called once per frame
    void Update()
    {
        GetTELEMETRYData();
    }

    void GetTELEMETRYData()
    {
        // Store the JSON string for parsing
        string telemetryJsonString = Data.GetTELEMETRYData();

        // Parse JSON string into TelemetryWrapper object
        // This stores the JSON value into the TelemetryWrapper object
        TelemetryWrapper telemetryWrapper = JsonUtility.FromJson<TelemetryWrapper>(telemetryJsonString);

        // Access specific values
        /*Debug.Log($"Deserialized Telemetry Data:\n" +
                  $"  EVA Time: {telemetryWrapper.telemetry.eva_time}\n" +
                  $"  EVA1 Oxy Pri Storage: {telemetryWrapper.telemetry.eva1.oxy_pri_storage}\n" +
                  $"  EVA1 Batt Time Left: {telemetryWrapper.telemetry.eva1.batt_time_left}\n" +
                  $"  EVA1 Oxy Sec Storage: {telemetryWrapper.telemetry.eva1.oxy_sec_storage}\n" +
                  $"  EVA1 Oxy Time Left: {telemetryWrapper.telemetry.eva1.oxy_time_left}\n"
        // Add more properties as needed
        );*/
    }
}
