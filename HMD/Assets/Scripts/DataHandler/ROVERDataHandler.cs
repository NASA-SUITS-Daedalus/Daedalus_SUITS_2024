using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// IMPORTANT: The TelemetryWrapper class is used to handle the additional
// wrapper layer in the received JSON string. The JSON structure includes
// an outer layer named "telemetry," and the TelemetryWrapper helps to
// unwrap and deserialize the data within that layer.
[System.Serializable]
public class ROVERWrapper
{
    // The telemetry field represents the inner data structure containing
    // the actual telemetry information we want to extract and use.
    public ROVERData rover;
}


[System.Serializable]
public class ROVERData
{
    public float posx;
    public float posy;
    public int qr_id;

}



public class ROVERDataHandler : MonoBehaviour
{
    // Reference to the TSSc Data Handler script
    public TSScDataHandler TSS;
    public ROVERWrapper roverWrapper;

    // Start is called before the first frame update
    void Start()
    {
        roverWrapper = new ROVERWrapper();
    }

// Update is called once per frame
void Update()
    {
        UpdateROVERData();
    }

    void UpdateROVERData()
    {
        // Store the JSON string for parsing
        string roverJsonString = TSS.GetROVERData();

        // Parse JSON string into TelemetryWrapper object
        // This stores the JSON value into the TelemetryWrapper object
        roverWrapper = JsonUtility.FromJson<ROVERWrapper>(roverJsonString);

        // Access specific values
        // Debug.Log($"posx: {GetPosx()}");
        // Debug.Log($"posy: {GetPosy()}");
        // Debug.Log($"qr_id: {GetQR_id()}");
    }

    // Use this function in other scripts to access the live telemetry data
    public float GetPosx()
    {
        return roverWrapper.rover.posx;
    }
    public float GetPosy()
    {
        return roverWrapper.rover.posy;
    }
    public int GetQR_id()
    {
        return roverWrapper.rover.qr_id;
    }
}