using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class RepairStatusDisplayer : MonoBehaviour
{

    // Communications readings
    public TextMeshProUGUI commReadings;

    // The telemetry data receiver
    public TELEMETRYDataHandler teleData;

    // The DCU data receiver
    public DCUDataHandler dcuData;

    // Formatter helper
    public ProcedureUtility helper;


    //// Start is called before the first frame update
    //void Start()
    //{
        
    //}

    // Update is called once per frame
    void Update()
    {
        // Update the communications statuses
        try
        {
            commReadings.text = string.Format("{0}\n{1}",
                helper.formatDCUComms(dcuData.GetComm("eva1")),
                helper.formatDCUComms(dcuData.GetComm("eva2"))
                );
        }
        catch
        {
            commReadings.text = string.Format("{0}\n{1}",
                helper.formatDCUComms(true),
                helper.formatDCUComms(true)
                );
        }
    }


}
