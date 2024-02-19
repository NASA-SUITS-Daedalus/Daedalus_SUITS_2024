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
    public TelemetryEvaData eva1;
    public TelemetryEvaData eva2;
}

[System.Serializable]
public class TelemetryEvaData
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
    public TSScDataHandler TSS;
    public TelemetryWrapper telemetryWrapper;

    // Start is called before the first frame update
    void Start()
    {
        telemetryWrapper = new TelemetryWrapper();
    }

// Update is called once per frame
void Update()
    {
        UpdateTELEMETRYData();
    }

    void UpdateTELEMETRYData()
    {
        // Store the JSON string for parsing
        string telemetryJsonString = TSS.GetTELEMETRYData();

        // Parse JSON string into TelemetryWrapper object
        // This stores the JSON value into the TelemetryWrapper object
        telemetryWrapper = JsonUtility.FromJson<TelemetryWrapper>(telemetryJsonString);
        // Debug.Log($" Heart Rate: {telemetryWrapper.telemetry.eva1.heart_rate}");

        // Access specific values
        // Debug.Log($"Deserialized Telemetry Data:\n" +
        //           $"  EVA Time: {telemetryWrapper.telemetry.eva_time}\n" +
        //           $"  EVA1 Oxy Pri Storage: {telemetryWrapper.telemetry.eva1.oxy_pri_storage}\n" +
        //           $"  EVA1 Batt Time Left: {telemetryWrapper.telemetry.eva1.batt_time_left}\n" +
        //           $"  EVA1 Oxy Sec Storage: {telemetryWrapper.telemetry.eva1.oxy_sec_storage}\n" +
        //           $"  EVA1 Oxy Time Left: {telemetryWrapper.telemetry.eva1.oxy_time_left}\n"
        // // Add more properties as needed
        // );
    
    }

    // Use this function in other scripts to access the live telemetry data
    public float GetHeartRate(string eva)
    {
        if (eva == "eva1" || eva == "eva2")
        {
            // Check which EVA data is requested
            TelemetryEvaData evaData = (eva == "eva1") ? telemetryWrapper.telemetry.eva1 : telemetryWrapper.telemetry.eva2;

            // Access the heart rate
            return evaData.heart_rate;
        }
        else
        {
            Debug.LogWarning("Invalid EVA specified. Use 'eva1' or 'eva2'.");
            return 0f; // Or another default value
        }
    }

    public float GetBattTimeLeft(string eva)
    {
        if (eva == "eva1" || eva == "eva2")
        {
            // Check which EVA data is requested
            TelemetryEvaData evaData = (eva == "eva1") ? telemetryWrapper.telemetry.eva1 : telemetryWrapper.telemetry.eva2;

            // Access the batt_time_left
            return evaData.batt_time_left;
        }
        else
        {
            Debug.LogWarning("Invalid EVA specified. Use 'eva1' or 'eva2'.");
            return 0f; // Or another default value
        }
    }

    public float GetOxyPriStorage(string eva)
    {
        if (eva == "eva1" || eva == "eva2")
        {
            // Check which EVA data is requested
            TelemetryEvaData evaData = (eva == "eva1") ? telemetryWrapper.telemetry.eva1 : telemetryWrapper.telemetry.eva2;

            // Access the oxy_pri_storage
            return evaData.oxy_pri_storage;
        }
        else
        {
            Debug.LogWarning("Invalid EVA specified. Use 'eva1' or 'eva2'.");
            return 0f; // Or another default value
        }
    }

    public float GetOxySecStorage(string eva)
    {
        if (eva == "eva1" || eva == "eva2")
        {
            // Check which EVA data is requested
            TelemetryEvaData evaData = (eva == "eva1") ? telemetryWrapper.telemetry.eva1 : telemetryWrapper.telemetry.eva2;

            // Access the batt_time_left
            return evaData.oxy_sec_storage;
        }
        else
        {
            Debug.LogWarning("Invalid EVA specified. Use 'eva1' or 'eva2'.");
            return 0f; // Or another default value
        }
    }

    public float GetOxyPriPressure(string eva)
    {
        if (eva == "eva1" || eva == "eva2")
        {
            // Check which EVA data is requested
            TelemetryEvaData evaData = (eva == "eva1") ? telemetryWrapper.telemetry.eva1 : telemetryWrapper.telemetry.eva2;

            // Access the oxy_pri_storage
            return evaData.oxy_pri_pressure;
        }
        else
        {
            Debug.LogWarning("Invalid EVA specified. Use 'eva1' or 'eva2'.");
            return 0f; // Or another default value
        }
    }

    public float GetOxySecPressure(string eva)
    {
        if (eva == "eva1" || eva == "eva2")
        {
            // Check which EVA data is requested
            TelemetryEvaData evaData = (eva == "eva1") ? telemetryWrapper.telemetry.eva1 : telemetryWrapper.telemetry.eva2;

            // Access the batt_time_left
            return evaData.oxy_sec_pressure;
        }
        else
        {
            Debug.LogWarning("Invalid EVA specified. Use 'eva1' or 'eva2'.");
            return 0f; // Or another default value
        }
    }

    public float GetOxyTimeLeft(string eva)
    {
        if (eva == "eva1" || eva == "eva2")
        {
            // Check which EVA data is requested
            TelemetryEvaData evaData = (eva == "eva1") ? telemetryWrapper.telemetry.eva1 : telemetryWrapper.telemetry.eva2;

            // Access the oxy_pri_storage
            return evaData.oxy_time_left;
        }
        else
        {
            Debug.LogWarning("Invalid EVA specified. Use 'eva1' or 'eva2'.");
            return 0f; // Or another default value
        }
    }

    public float GetOxyConsumption(string eva)
    {
        if (eva == "eva1" || eva == "eva2")
        {
            // Check which EVA data is requested
            TelemetryEvaData evaData = (eva == "eva1") ? telemetryWrapper.telemetry.eva1 : telemetryWrapper.telemetry.eva2;

            // Access the batt_time_left
            return evaData.oxy_consumption;
        }
        else
        {
            Debug.LogWarning("Invalid EVA specified. Use 'eva1' or 'eva2'.");
            return 0f; // Or another default value
        }
    }

    public float GetCO2Production(string eva)
    {
        if (eva == "eva1" || eva == "eva2")
        {
            // Check which EVA data is requested
            TelemetryEvaData evaData = (eva == "eva1") ? telemetryWrapper.telemetry.eva1 : telemetryWrapper.telemetry.eva2;

            // Access the oxy_pri_storage
            return evaData.co2_production;
        }
        else
        {
            Debug.LogWarning("Invalid EVA specified. Use 'eva1' or 'eva2'.");
            return 0f; // Or another default value
        }
    }

    public float GetSuitPressureOxy(string eva)
    {
        if (eva == "eva1" || eva == "eva2")
        {
            // Check which EVA data is requested
            TelemetryEvaData evaData = (eva == "eva1") ? telemetryWrapper.telemetry.eva1 : telemetryWrapper.telemetry.eva2;

            // Access the batt_time_left
            return evaData.suit_pressure_oxy;
        }
        else
        {
            Debug.LogWarning("Invalid EVA specified. Use 'eva1' or 'eva2'.");
            return 0f; // Or another default value
        }
    }

    public float GetSuitPressureCO2(string eva)
    {
        if (eva == "eva1" || eva == "eva2")
        {
            // Check which EVA data is requested
            TelemetryEvaData evaData = (eva == "eva1") ? telemetryWrapper.telemetry.eva1 : telemetryWrapper.telemetry.eva2;

            // Access the oxy_pri_storage
            return evaData.suit_pressure_co2;
        }
        else
        {
            Debug.LogWarning("Invalid EVA specified. Use 'eva1' or 'eva2'.");
            return 0f; // Or another default value
        }
    }

    public float GetSuitPressureOther(string eva)
    {
        if (eva == "eva1" || eva == "eva2")
        {
            // Check which EVA data is requested
            TelemetryEvaData evaData = (eva == "eva1") ? telemetryWrapper.telemetry.eva1 : telemetryWrapper.telemetry.eva2;

            // Access the batt_time_left
            return evaData.suit_pressure_other;
        }
        else
        {
            Debug.LogWarning("Invalid EVA specified. Use 'eva1' or 'eva2'.");
            return 0f; // Or another default value
        }
    }

    public float GetSuitPresssureTotal(string eva)
    {
        if (eva == "eva1" || eva == "eva2")
        {
            // Check which EVA data is requested
            TelemetryEvaData evaData = (eva == "eva1") ? telemetryWrapper.telemetry.eva1 : telemetryWrapper.telemetry.eva2;

            // Access the oxy_pri_storage
            return evaData.suit_pressure_total;
        }
        else
        {
            Debug.LogWarning("Invalid EVA specified. Use 'eva1' or 'eva2'.");
            return 0f; // Or another default value
        }
    }

    public float GetFanPriRpm(string eva)
    {
        if (eva == "eva1" || eva == "eva2")
        {
            // Check which EVA data is requested
            TelemetryEvaData evaData = (eva == "eva1") ? telemetryWrapper.telemetry.eva1 : telemetryWrapper.telemetry.eva2;

            // Access the batt_time_left
            return evaData.fan_pri_rpm;
        }
        else
        {
            Debug.LogWarning("Invalid EVA specified. Use 'eva1' or 'eva2'.");
            return 0f; // Or another default value
        }
    }

    public float GetFanSecRpm(string eva)
    {
        if (eva == "eva1" || eva == "eva2")
        {
            // Check which EVA data is requested
            TelemetryEvaData evaData = (eva == "eva1") ? telemetryWrapper.telemetry.eva1 : telemetryWrapper.telemetry.eva2;

            // Access the oxy_pri_storage
            return evaData.fan_sec_rpm;
        }
        else
        {
            Debug.LogWarning("Invalid EVA specified. Use 'eva1' or 'eva2'.");
            return 0f; // Or another default value
        }
    }

    public float GetHelmetPressure(string eva)
    {
        if (eva == "eva1" || eva == "eva2")
        {
            // Check which EVA data is requested
            TelemetryEvaData evaData = (eva == "eva1") ? telemetryWrapper.telemetry.eva1 : telemetryWrapper.telemetry.eva2;

            // Access the batt_time_left
            return evaData.helmet_pressure_co2;
        }
        else
        {
            Debug.LogWarning("Invalid EVA specified. Use 'eva1' or 'eva2'.");
            return 0f; // Or another default value
        }
    }

    public float GetScrubber_A_CO2Storage(string eva)
    {
        if (eva == "eva1" || eva == "eva2")
        {
            // Check which EVA data is requested
            TelemetryEvaData evaData = (eva == "eva1") ? telemetryWrapper.telemetry.eva1 : telemetryWrapper.telemetry.eva2;

            // Access the oxy_pri_storage
            return evaData.scrubber_a_co2_storage;
        }
        else
        {
            Debug.LogWarning("Invalid EVA specified. Use 'eva1' or 'eva2'.");
            return 0f; // Or another default value
        }
    }

    public float GetScrubber_B_CO2Storage(string eva)
    {
        if (eva == "eva1" || eva == "eva2")
        {
            // Check which EVA data is requested
            TelemetryEvaData evaData = (eva == "eva1") ? telemetryWrapper.telemetry.eva1 : telemetryWrapper.telemetry.eva2;

            // Access the batt_time_left
            return evaData.scrubber_b_co2_storage;
        }
        else
        {
            Debug.LogWarning("Invalid EVA specified. Use 'eva1' or 'eva2'.");
            return 0f; // Or another default value
        }
    }

    public float GetTemparature(string eva)
    {
        if (eva == "eva1" || eva == "eva2")
        {
            // Check which EVA data is requested
            TelemetryEvaData evaData = (eva == "eva1") ? telemetryWrapper.telemetry.eva1 : telemetryWrapper.telemetry.eva2;

            // Access the oxy_pri_storage
            return evaData.temperature;
        }
        else
        {
            Debug.LogWarning("Invalid EVA specified. Use 'eva1' or 'eva2'.");
            return 0f; // Or another default value
        }
    }

    public float GetCoolantMl(string eva)
    {
        if (eva == "eva1" || eva == "eva2")
        {
            // Check which EVA data is requested
            TelemetryEvaData evaData = (eva == "eva1") ? telemetryWrapper.telemetry.eva1 : telemetryWrapper.telemetry.eva2;

            // Access the batt_time_left
            return evaData.coolant_ml;
        }
        else
        {
            Debug.LogWarning("Invalid EVA specified. Use 'eva1' or 'eva2'.");
            return 0f; // Or another default value
        }
    }

    public float GetCoolantGasPressure(string eva)
    {
        if (eva == "eva1" || eva == "eva2")
        {
            // Check which EVA data is requested
            TelemetryEvaData evaData = (eva == "eva1") ? telemetryWrapper.telemetry.eva1 : telemetryWrapper.telemetry.eva2;

            // Access the oxy_pri_storage
            return evaData.coolant_gas_pressure;
        }
        else
        {
            Debug.LogWarning("Invalid EVA specified. Use 'eva1' or 'eva2'.");
            return 0f; // Or another default value
        }
    }

    public float GetCoolantLiquidPressure(string eva)
    {
        if (eva == "eva1" || eva == "eva2")
        {
            // Check which EVA data is requested
            TelemetryEvaData evaData = (eva == "eva1") ? telemetryWrapper.telemetry.eva1 : telemetryWrapper.telemetry.eva2;

            // Access the batt_time_left
            return evaData.coolant_liquid_pressure;
        }
        else
        {
            Debug.LogWarning("Invalid EVA specified. Use 'eva1' or 'eva2'.");
            return 0f; // Or another default value
        }
    }
}
