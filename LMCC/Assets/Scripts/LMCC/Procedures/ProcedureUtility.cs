using UnityEngine;
using System.Collections;

/*
 * A helper class for the procedure interface.
 * 
 * Primarily contains functions to help format data read from the TSS.
 * 
 */
public class ProcedureUtility : MonoBehaviour
{

    // Start
    void Start()
    {

    }

    /* FORMATTING HELPERS */

    /*
     * Format a switch status as an easily readable string. 
     */
    public string formatSwitch(bool status=false)
    {
        if (status) { return "ON"; }
        else { return "OFF"; }
    }

    /*
     * Format a connection status as an easily readable string.
     */
    public string formatConnection(bool status = false)
    {
        if (status) { return "CONNECTED"; }
        else { return "NOT CONNECTED"; }
    }

    /*
     * Format a valve status as an easily readable string. 
     */
    public string formatValve(bool status = false)
    {
        if (status) { return "OPEN"; }
        else { return "CLOSED";  }
    }

    /*
     * Format a pressure measurement as an easily readable string. 
     */
    public string formatPressure(float pressure=0f, int places = 1)
    {
        return string.Format("{0:0.0#}", pressure) + " psi";
    }

    /*
     * Format a float as an easily readable percentage. 
     */
    public string formatPercentage(float value=0f, int places = 1)
    {
        return string.Format("{0:0.0#}", value) + "%";
    }


    /* DCU FORMATTERS */

    /*
     * Format the DCU battery status as an easily readable string.
     */
    public string formatDCUBatt(bool status = true)
    {
        // TODO check to see if this is correct
        if (status) { return "LOCAL"; }
        else { return "UMBILICAL"; }
    }

    /*
     * Format the DCU active oxygen tank as an easily readable string. 
     */
    public string formatDCUOxy(bool status = true)
    {
        // TODO check to see if this is correct
        if (status) { return "PRI"; }
        else { return "SEC"; }
    }

    /*
     * Format the DCU communications status as an easily readable string. 
     */
    public string formatDCUComms(bool status = true)
    {
        // TODO check to see if this is correct
        if (status) { return "A"; }
        else { return "-"; }
    }

    /*
     * Format the DCU active fan as an easily readable string. 
     */
    public string formatDCUFan(bool status = true)
    {
        // TODO check to see if this is correct
        if (status) { return "PRI"; }
        else { return "SEC"; }
    }

    /*
     * Format the DCU pump status as an easily readable string. 
     */
    public string formatDCUPump(bool status = false)
    {
        // TODO check to see if this is correct
        if (status) { return "OPEN"; }
        else { return "CLOSED"; }
    }

    /*
     * Format the DCU CO2 scrubber status as an easily readable string. 
     */
    public string formatDCUCO2(bool status = true)
    {
        // TODO check to see if this is correct
        if (status) { return "A"; }
        else { return "-"; }
    }

}

