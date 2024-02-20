using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// IMPORTANT: The TelemetryWrapper class is used to handle the additional
// wrapper layer in the received JSON string. The JSON structure includes
// an outer layer named "telemetry," and the TelemetryWrapper helps to
// unwrap and deserialize the data within that layer.
[System.Serializable]
public class COMMWrapper
{
    // The telemetry field represents the inner data structure containing
    // the actual telemetry information we want to extract and use.
    public COMMData comm;
}


[System.Serializable]
public class COMMData
{
    public bool comm_tower;

}



public class COMMDataHandler : MonoBehaviour
{
    // Reference to the TSSc Data Handler script
    public TSScDataHandler TSS;
    public COMMWrapper commWrapper;

    // Start is called before the first frame update
    void Start()
    {
        commWrapper = new COMMWrapper();
    }

// Update is called once per frame
void Update()
    {
        UpdateCOMMData();
    }

    void UpdateCOMMData()
    {
        // Store the JSON string for parsing
        string commJsonString = TSS.GetCOMMData();

        // Parse JSON string into TelemetryWrapper object
        // This stores the JSON value into the TelemetryWrapper object
        commWrapper = JsonUtility.FromJson<COMMWrapper>(commJsonString);

        // Access specific values
        //Debug.Log($"comm_tower: {GetComm_Tower()}");
        
    }

    // Use this function in other scripts to access the live telemetry data
    public bool GetComm_Tower()
    {
        return commWrapper.comm.comm_tower;
    }
    
}