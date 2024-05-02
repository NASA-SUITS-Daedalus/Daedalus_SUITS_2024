using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// IMPORTANT: The TelemetryWrapper class is used to handle the additional
// wrapper layer in the received JSON string. The JSON structure includes
// an outer layer named "telemetry," and the TelemetryWrapper helps to
// unwrap and deserialize the data within that layer.
[System.Serializable]
public class UIAWrapper
{
    // The telemetry field represents the inner data structure containing
    // the actual telemetry information we want to extract and use.
    public UIAData uia;
}


[System.Serializable]
public class UIAData
{
    public bool eva1_power;
    public bool eva1_oxy;
    public bool eva1_water_supply;
    public bool eva1_water_waste;
    public bool eva2_power;
    public bool eva2_oxy;
    public bool eva2_water_supply;
    public bool eva2_water_waste;
    public bool oxy_vent;
    public bool depress;

}



public class UIADataHandler : MonoBehaviour
{
    // Reference to the TSSc Data Handler script
    public TSScDataHandler TSS;
    public UIAWrapper uiaWrapper;

    // Start is called before the first frame update
    void Start()
    {
        uiaWrapper = new UIAWrapper();
    }

// Update is called once per frame
void Update()
    {
        UpdateUIAData();
    }

    void UpdateUIAData()
    {
        // Store the JSON string for parsing
        string uiaJsonString = TSS.GetUIAData();

        // Parse JSON string into TelemetryWrapper object
        // This stores the JSON value into the TelemetryWrapper object
        uiaWrapper = JsonUtility.FromJson<UIAWrapper>(uiaJsonString);

        // Access specific values
        // Debug.Log($"eva1_power: {GetPower("eva1")}");
        // Debug.Log($"eva1_oxy: {GetOxy("eva1")}");
        // Debug.Log($"eva1_water_waste: {GetWater_Waste("eva1")}");
        // Debug.Log($"eva1_water_supply: {GetWater_Supply("eva1")}");
        // Debug.Log($"Oxy_Vent: {GetOxy_Vent()}");
        // Debug.Log($"Depress: {GetDepress()}");
    }

    // Use this function in other scripts to access the live telemetry data
    public bool GetPower(string eva)
    {
        if (eva == "eva1")
        {
            return uiaWrapper.uia.eva1_power;
        }
        else if (eva == "eva2")
        {
            return uiaWrapper.uia.eva2_power;
        }
        else 
        {
            Debug.LogWarning("error: specify eva1 or eva2");
            return false;
        }
    }
    public bool GetOxy(string eva)
    {
        if (eva == "eva1")
        {
            return uiaWrapper.uia.eva1_oxy;
        }
        else if (eva == "eva2")
        {
            return uiaWrapper.uia.eva2_oxy;
        }
        else 
        {
            Debug.LogWarning("error: specify eva1 or eva2");
            return false;
        }
    }
    public bool GetWater_Supply(string eva)
    {
        if (eva == "eva1")
        {
            return uiaWrapper.uia.eva1_water_supply;
        }
        else if (eva == "eva2")
        {
            return uiaWrapper.uia.eva2_water_supply;
        }
        else 
        {
            Debug.LogWarning("error: specify eva1 or eva2");
            return false;
        }
    }
    public bool GetWater_Waste(string eva)
    {
        if (eva == "eva1")
        {
            return uiaWrapper.uia.eva1_water_waste;
        }
        else if (eva == "eva2")
        {
            return uiaWrapper.uia.eva2_water_waste;
        }
        else 
        {
            Debug.LogWarning("error: specify eva1 or eva2");
            return false;
        }
    }
    public bool GetOxy_Vent()
    {
        return uiaWrapper.uia.oxy_vent;
    }
    public bool GetDepress()
    {
        return uiaWrapper.uia.depress;
    }
}
