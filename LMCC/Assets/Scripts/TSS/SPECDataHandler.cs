
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// IMPORTANT: The TelemetryWrapper class is used to handle the additional
// wrapper layer in the received JSON string. The JSON structure includes
// an outer layer named "telemetry," and the TelemetryWrapper helps to
// unwrap and deserialize the data within that layer.
[System.Serializable]
public class SPECWrapper
{
    // The telemetry field represents the inner data structure containing
    // the actual telemetry information we want to extract and use.
    public SPECData spec;
}

[System.Serializable]
public class SPECData
{
    public SPECEva eva1;
    public SPECEva eva2;
}

[System.Serializable]
public class SPECEva
{
    public string name;
    public float id;
    public SPECSampleData data;
}

[System.Serializable]
public class SPECSampleData
{
    public float SiO2;
    public float TiO2;
    public float Al2O3;
    public float FeO;
    public float MnO;
    public float MgO;
    public float CaO;
    public float K2O;
    public float P2O3;
    public float other;
}


public class SPECDataHandler : MonoBehaviour
{
    // Reference to the TSSc Data Handler script
    public TSScDataHandler TSS;
    public SPECWrapper specWrapper;

    // Start is called before the first frame update
    void Start()
    {
        specWrapper = new SPECWrapper();
    }
// Update is called once per frame
void Update()
    {
        UpdateSPECData();
    }

    void UpdateSPECData()
    {
        // Store the JSON string for parsing
        string specJsonString = TSS.GetSPECData();
       
        
        // Parse JSON string into TelemetryWrapper object
        // This stores the JSON value into the TelemetryWrapper object
        specWrapper = JsonUtility.FromJson<SPECWrapper>(specJsonString);

        // Access specific values
        // Debug.Log($"Eva1_Name: {GetName("eva1")}");
        // Debug.Log($"Eva1_Id: {GetId("eva1")}");
        // Debug.Log($"Eva1_data_SiO2: {GetCompoundData("eva1","SiO2")}");
        // Debug.Log($"Eva1_data_TiO2: {GetCompoundData("eva1","TiO2")}");
        // Debug.Log($"Eva1_data_Al2O3: {GetCompoundData("eva1","Al2O3")}");
        // Debug.Log($"Eva1_data_FeO: {GetCompoundData("eva1","FeO")}");
        // Debug.Log($"Eva1_data_MnO: {GetCompoundData("eva1","MnO")}");
        // Debug.Log($"Eva1_data_MgO: {GetCompoundData("eva1","MgO")}");
        // Debug.Log($"Eva1_data_CaO: {GetCompoundData("eva1","CaO")}");
        // Debug.Log($"Eva1_data_K2O: {GetCompoundData("eva1","K2O")}");
        // Debug.Log($"Eva1_data_P2O3: {GetCompoundData("eva1","P2O3")}");
        // Debug.Log($"Eva1_data_other: {GetCompoundData("eva1","other")}");
        
    }
    // Use this function in other scripts to access the live telemetry data
    public string GetName(string eva)
    {
        if (eva == "eva1" || eva == "eva2")
        {
            // Check which EVA data is requested
            SPECEva evaData = (eva == "eva1") ? specWrapper.spec.eva1 : specWrapper.spec.eva2;

            // Access the heart rate
            return evaData.name;
        }
        else
        {
            Debug.LogWarning("Invalid EVA specified. Use 'eva1' or 'eva2'.");
            return null; // Or another default value
        }
    }
    public float GetId(string eva)
    {
        if (eva == "eva1" || eva == "eva2")
        {
            // Check which EVA data is requested
            SPECEva evaData = (eva == "eva1") ? specWrapper.spec.eva1 : specWrapper.spec.eva2;

            // Access the heart rate
            return evaData.id;
        }
        else
        {
            Debug.LogWarning("Invalid EVA specified. Use 'eva1' or 'eva2'.");
            return 0f; // Or another default value
        }
    }
    public float GetCompoundData(string eva, string compound)
    {
        if (eva == "eva1" || eva == "eva2")
        {
            // Check which EVA data is requested
            SPECEva evaData = (eva == "eva1") ? specWrapper.spec.eva1 : specWrapper.spec.eva2;

            switch (compound)
            {
                case "SiO2":
                    return evaData.data.SiO2;
                case "TiO2":
                    return evaData.data.TiO2;
                case "Al2O3":
                    return evaData.data.Al2O3;
                case "FeO":
                    return evaData.data.FeO;
                case "MnO":
                    return evaData.data.MnO;
                case "MgO":
                    return evaData.data.MgO;
                case "CaO":
                    return evaData.data.CaO;
                case "K2O":
                    return evaData.data.K2O;
                case "P2O3":
                    return evaData.data.P2O3;
                case "other":
                    return evaData.data.other;
                default:
                    Debug.LogWarning("Invalid compound specified. Use one in [SiO2,TiO2,Al2O3,FeO,MnO,MgO,CaO,K2O,P2O3,other]");
                    return 0f; // Or another default value
            }
        }
        else
        {
            Debug.LogWarning("Invalid EVA specified. Use 'eva1' or 'eva2'.");
            return 0f; // Or another default value
        }
    }
}