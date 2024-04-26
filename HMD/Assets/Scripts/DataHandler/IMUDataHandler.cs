
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// IMPORTANT: The TelemetryWrapper class is used to handle the additional
// wrapper layer in the received JSON string. The JSON structure includes
// an outer layer named "telemetry," and the TelemetryWrapper helps to
// unwrap and deserialize the data within that layer.
[System.Serializable]
public class IMUWrapper
{
    // The telemetry field represents the inner data structure containing
    // the actual telemetry information we want to extract and use.
    public IMUData imu;
}

[System.Serializable]
public class IMUData
{
    public IMUEvaData eva1;
    public IMUEvaData eva2;
}

[System.Serializable]
public class IMUEvaData
{
    public float posx;
    public float posy;
    public float heading;

}



public class IMUDataHandler : MonoBehaviour
{
    // Reference to the TSSc Data Handler script
    public TSScDataHandler TSS;
    public IMUWrapper imuWrapper;

    // Start is called before the first frame update
    void Start()
    {
        imuWrapper = new IMUWrapper();
    }
// Update is called once per frame
void Update()
    {
        UpdateIMUData();
    }

    void UpdateIMUData()
    {
        // Store the JSON string for parsing
        string imuJsonString = TSS.GetIMUData();
       
        
        // Parse JSON string into TelemetryWrapper object
        // This stores the JSON value into the TelemetryWrapper object
        imuWrapper = JsonUtility.FromJson<IMUWrapper>(imuJsonString);

        // Access specific values
        // Debug.Log($"Eva1_Posx: {GetPosx("eva1")}");
        // Debug.Log($"Eva1_Posy: {GetPosy("eva1")}");
        // Debug.Log($"Eva1_Heading: {GetHeading("eva1")}");
        
        
        
    }
    // Use this function in other scripts to access the live telemetry data
    public float GetPosx(string eva)
    {
        if (eva == "eva1" || eva == "eva2")
        {
            // Check which EVA data is requested
            IMUEvaData evaData = (eva == "eva1") ? imuWrapper.imu.eva1 : imuWrapper.imu.eva2;

            // Access the heart rate
            return evaData.posx;
        }
        else
        {
            Debug.LogWarning("Invalid EVA specified. Use 'eva1' or 'eva2'.");
            return 0f; // Or another default value
        }
    }
    public float GetPosy(string eva)
    {
        if (eva == "eva1" || eva == "eva2")
        {
            // Check which EVA data is requested
            IMUEvaData evaData = (eva == "eva1") ? imuWrapper.imu.eva1 : imuWrapper.imu.eva2;

            // Access the heart rate
            return evaData.posy;
        }
        else
        {
            Debug.LogWarning("Invalid EVA specified. Use 'eva1' or 'eva2'.");
            return 0f; // Or another default value
        }
    }
    public float GetHeading(string eva)
    {
        if (eva == "eva1" || eva == "eva2")
        {
            // Check which EVA data is requested
            IMUEvaData evaData = (eva == "eva1") ? imuWrapper.imu.eva1 : imuWrapper.imu.eva2;

            // Access the heart rate
            return evaData.heading;
        }
        else
        {
            Debug.LogWarning("Invalid EVA specified. Use 'eva1' or 'eva2'.");
            return 0f; // Or another default value
        }
    }
}