
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// IMPORTANT: The TelemetryWrapper class is used to handle the additional
// wrapper layer in the received JSON string. The JSON structure includes
// an outer layer named "telemetry," and the TelemetryWrapper helps to
// unwrap and deserialize the data within that layer.
[System.Serializable]
public class DCUWrapper
{
    // The telemetry field represents the inner data structure containing
    // the actual telemetry information we want to extract and use.
    public DCUData dcu;
}

[System.Serializable]
public class DCUData
{
    public DCUEvaData eva1;
    public DCUEvaData eva2;
}

[System.Serializable]
public class DCUEvaData
{
    public bool batt;
    public bool oxy;
    public bool comm;
    public bool fan;
    public bool pump;
    public bool co2;

}



public class DCUDataHandler : MonoBehaviour
{
    // Reference to the TSSc Data Handler script
    public TSScDataHandler TSS;
    public DCUWrapper dcuWrapper;

    // Start is called before the first frame update
    void Start()
    {
        dcuWrapper = new DCUWrapper();
    }
// Update is called once per frame
void Update()
    {
        UpdateDCUData();
    }

    void UpdateDCUData()
    {
        // Store the JSON string for parsing
        string dcuJsonString = TSS.GetDCUData();
       
        
        // Parse JSON string into TelemetryWrapper object
        // This stores the JSON value into the TelemetryWrapper object
        dcuWrapper = JsonUtility.FromJson<DCUWrapper>(dcuJsonString);

        // Access specific values
        // Debug.Log($"Eva1_Batt: {GetBatt("eva1")}");
        // Debug.Log($"Eva2_Batt: {GetBatt("eva2")}");
        // Debug.Log($"Eva1_Oxy: {GetOxy("eva1")}");
        // Debug.Log($"Eva2_Oxy: {GetOxy("eva2")}");
        // Debug.Log($"Eva1_Comm: {GetComm("eva1")}");
        // Debug.Log($"Eva2_Comm: {GetComm("eva2")}");
        // Debug.Log($"Eva1_Fan: {GetFan("eva1")}");
        // Debug.Log($"Eva2_Fan: {GetFan("eva2")}");
        // Debug.Log($"Eva1_Pump: {GetPump("eva1")}");
        // Debug.Log($"Eva2_Pump: {GetPump("eva2")}");
        // Debug.Log($"Eva1_CO2: {GetCO2("eva1")}");
        // Debug.Log($"Eva2_CO2: {GetCO2("eva2")}");
        
        
    }
    // Use this function in other scripts to access the live telemetry data
    public bool GetBatt(string eva)
    {
        if (eva == "eva1" || eva == "eva2")
        {
            // Check which EVA data is requested
            DCUEvaData evaData = (eva == "eva1") ? dcuWrapper.dcu.eva1 : dcuWrapper.dcu.eva2;

            return evaData.batt;
        }
        else
        {
            Debug.LogWarning("Invalid EVA specified. Use 'eva1' or 'eva2'.");
            return false; // Or another default value
        }
    }
    public bool GetOxy(string eva)
    {
        if (eva == "eva1" || eva == "eva2")
        {
            // Check which EVA data is requested
            DCUEvaData evaData = (eva == "eva1") ? dcuWrapper.dcu.eva1 : dcuWrapper.dcu.eva2;

            return evaData.oxy;
        }
        else
        {
            Debug.LogWarning("Invalid EVA specified. Use 'eva1' or 'eva2'.");
            return false; // Or another default value
        }
    }
    public bool GetComm(string eva)
    {
        if (eva == "eva1" || eva == "eva2")
        {
            // Check which EVA data is requested
            DCUEvaData evaData = (eva == "eva1") ? dcuWrapper.dcu.eva1 : dcuWrapper.dcu.eva2;

            return evaData.comm;
        }
        else
        {
            Debug.LogWarning("Invalid EVA specified. Use 'eva1' or 'eva2'.");
            return false; // Or another default value
        }
    }
    public bool GetFan(string eva)
    {
        if (eva == "eva1" || eva == "eva2")
        {
            // Check which EVA data is requested
            DCUEvaData evaData = (eva == "eva1") ? dcuWrapper.dcu.eva1 : dcuWrapper.dcu.eva2;

            return evaData.fan;
        }
        else
        {
            Debug.LogWarning("Invalid EVA specified. Use 'eva1' or 'eva2'.");
            return false; // Or another default value
        }
    }
    public bool GetPump(string eva)
    {
        if (eva == "eva1" || eva == "eva2")
        {
            // Check which EVA data is requested
            DCUEvaData evaData = (eva == "eva1") ? dcuWrapper.dcu.eva1 : dcuWrapper.dcu.eva2;

            return evaData.pump;
        }
        else
        {
            Debug.LogWarning("Invalid EVA specified. Use 'eva1' or 'eva2'.");
            return false; // Or another default value
        }
    }
    public bool GetCO2(string eva)
    {
        if (eva == "eva1" || eva == "eva2")
        {
            // Check which EVA data is requested
            DCUEvaData evaData = (eva == "eva1") ? dcuWrapper.dcu.eva1 : dcuWrapper.dcu.eva2;

            return evaData.co2;
        }
        else
        {
            Debug.LogWarning("Invalid EVA specified. Use 'eva1' or 'eva2'.");
            return false; // Or another default value
        }
    }
}