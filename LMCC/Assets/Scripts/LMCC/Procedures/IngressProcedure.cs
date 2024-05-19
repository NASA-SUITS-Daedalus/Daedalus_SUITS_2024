using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class IngressProcedure : Procedure
{
    // Start is called before the first frame update
    void Start()
    {
        // TODO delete, this is just for testing
        //startProcedure();

        taskChecks = new List<System.Action>
        {
            step1Task1,
            step1Task2,
            step1Task3,
            step2Task1,
            step2Task2,
            step2Task3,
            step3Task1,
            step3Task2,
            step3Task3,
            step3Task4,
            step4Task1
        };
    }


    /*
     * Ingress procedure step 1.1
     * EV-1 and EV-2: Connect UIA to DCU using the umbilical cable. 
     * Each EV needs to connect to the UIA via the umbilical cable. 
     */
    protected void step1Task1()
    {
        info = "Each EV needs to connect to the UIA via the umbilical cable.\n" +
            "Once connected, use the UIA's EV-1 and EV-2 power switches to power " +
            "each suit.";

        // Get the power status of each suit from UIA
        bool eva1Status;
        bool eva2Status;

        try
        {
            eva1Status = uiaData.GetPower("eva1");
            eva2Status = uiaData.GetPower("eva2");
        }

        catch (Exception e)
        {
            //Debug.Log("Exception in ingress procedure step 1.1: " + e.Message);
            eva1Status = false;
            eva2Status = false;
        }

        advanceFlag = (eva1Status && eva2Status);
        status = string.Format("EV-1 PWR status: {0}\nEV-2 PWR status: {1}",
                                helper.formatSwitch(eva1Status),
                                helper.formatSwitch(eva2Status));
    }

    /*
     * Ingress procedure step 1.2
     * Both DCU: Switch BATT to UMB.
     * For each DCU, switch the battery mode to umbilical. 
     */
    protected void step1Task2()
    {
        info = "For each DCU, switch the battery mode to umbilical.";

        // Get the current battery status of each DCU (local or umbilical)
        bool eva1Status;
        bool eva2Status;

        try
        {
            eva1Status = dcuData.GetBatt("eva1");
            eva2Status = dcuData.GetBatt("eva2");
        }

        catch (Exception e)
        {
            //Debug.Log("Exception in ingress procedure step 1.2: " + e.Message);
            eva1Status = false;
            eva2Status = false;
        }

        advanceFlag = !(eva1Status || eva2Status);
        status = string.Format("EV-1 DCU-BATT status: {0}\nEV-2 DCU-BATT status: {1}",
                                helper.formatDCUBatt(eva1Status),
                                helper.formatDCUBatt(eva2Status));
    }

    /*
     * Egress procedure step 1.3
     * Switch the DEPRESS PUMP PWR to ON. 
     * Turn on the depress pump power to begin the depress of each suit. 
     */
    protected void step1Task3()
    {

        info = "Turn on the depress pump power to begin the depress of each suit.";

        // Get the UIA's depress pump status
        bool depressStatus;

        try
        {
            depressStatus = uiaData.GetDepress();
        }

        catch (Exception e)
        {
            //Debug.Log("Exception in ingress procedure step 1.3: " + e.Message);
            depressStatus = false;
        }

        advanceFlag = depressStatus;
        status = string.Format("UIA depress pump power status: {0}",
            helper.formatSwitch(depressStatus));

    }


    /*
     * Ingress procedure step 2.1
     * UIA: Switch the OXYGEN/O2 VENT to OPEN.
     * Open the oxygen vent on the UIA. 
     */
    protected void step2Task1()
    {
        info = "Open the oxygen vent on the UIA.";

        // Get the UIA's oxy vent status
        bool oxyVentStatus; 

        try
        {
            oxyVentStatus = uiaData.GetOxy_Vent();
        }

        catch (Exception e)
        {
            //Debug.Log("Exception in ingress procedure step 2.1: " + e.Message);
            oxyVentStatus = false;
        }

        advanceFlag = (oxyVentStatus == true);
        status = string.Format("UIA O2 vent status: {0}",
            helper.formatSwitch(oxyVentStatus));
    }

    /*
     * Ingress procedure step 2.2
     * Wait until both primary and secondary O2 tanks are < 10 psi…
     * Wait until the primary and secondary oxygen tank pressures for both EVs 
     * are under 10 psi.
     */
    protected void step2Task2()
    {
        info = "Wait until the primary and secondary oxygen tank pressures for both EVs are under 10 psi.";

        // Get the tank readings
        float ev1Primary;
        float ev1Secondary;
        float ev2Primary;
        float ev2Secondary;

        try
        {
            ev1Primary = teleData.GetOxyPriPressure("eva1");
            ev1Secondary = teleData.GetOxySecPressure("eva1");
            ev2Primary = teleData.GetOxyPriPressure("eva2");
            ev2Secondary = teleData.GetOxySecPressure("eva2");
        }

        catch
        {
            ev1Primary = 0f;
            ev1Secondary = 0f;
            ev2Primary = 0f;
            ev2Secondary = 0f;
        }

        advanceFlag = (ev1Primary < 10f &&
                        ev1Secondary < 10f &&
                        ev2Primary < 10f &&
                        ev2Secondary < 10f);

        status = string.Format("EV-1 primary O2 tank pressure: {0}\n" +
                                "EV-1 secondary O2 tank pressure: {1}\n" +
                                "EV-2 primary O2 tank pressure: {2}\n" +
                                "EV-2 secondary O2 tank pressure: {3}\n",
                                helper.formatPressure(ev1Primary),
                                helper.formatPressure(ev1Secondary),
                                helper.formatPressure(ev2Primary),
                                helper.formatPressure(ev2Secondary)
                                );
    }

    /*
     * Ingress procedure step 2.3
     * UIA: Switch the OXYGEN O2 VENT to CLOSED.
     * Close the oxygen vent on the UIA. 
     */
    protected void step2Task3()
    {
        info = "UIA: Switch the OXYGEN O2 VENT to CLOSED.";

        // Check the status of the oxygen vent
        bool oxyVentStatus;

        try
        {
            oxyVentStatus = uiaData.GetOxy_Vent();
        }
        catch
        {
            oxyVentStatus = false;
        }

        advanceFlag = oxyVentStatus == false;
        status = string.Format("UIA O2 vent status: {0}",
            helper.formatSwitch(oxyVentStatus));

    }

    /*
     * Ingress procedure step 3.1
     * Both DCU: Switch PUMP to OPEN.
     * For each DCU, open the water pump to allow coolant to flow between 
     * the EVs’ suits and the UIA. 
     */
    protected void step3Task1()
    {
        info = "For each DCU, open the water pump to allow coolant to flow " +
            "between the EVs’ suits and the UIA.";

        bool eva1Pump;
        bool eva2Pump;

        try
        {
            eva1Pump = dcuData.GetPump("eva1");
            eva2Pump = dcuData.GetPump("eva2");
        }
        catch
        {
            eva1Pump = false;
            eva2Pump = false;
        }

        advanceFlag = (eva1Pump && eva2Pump);
        status = string.Format("UIA EMU-1 water pump status: {0}\n" +
                                "UIA EMU-1 water pump status: {1}",
                                helper.formatValve(eva1Pump),
                                helper.formatValve(eva2Pump)
                                );
    }

    /*
     * Ingress procedure step 3.2
     * UIA: Switch EV-1, EV-2 WASTE WATER to OPEN. 
     * Turn on each EV’s waste water valve using the UIA to begin flushing 
     * the waste water. 
     */
    protected void step3Task2()
    {
        info = "Turn on each EV’s waste water valve using the UIA to begin " +
            "flushing the waste water. ";

        // Get the waste water valve status
        bool eva1WasteWater;
        bool eva2WasteWater;

        try
        {
            eva1WasteWater = uiaData.GetWater_Waste("eva1");
            eva2WasteWater = uiaData.GetWater_Waste("eva2");
        }

        catch
        {
            eva1WasteWater = false;
            eva2WasteWater = false;
        }

        advanceFlag = (eva1WasteWater && eva2WasteWater);
        status = string.Format("UIA EMU-1 waste water valve status: {0}\n" +
                                "UIA EMU-1 waste water valve status: {1}",
                                helper.formatValve(eva1WasteWater),
                                helper.formatValve(eva2WasteWater)
                                );
    }

    /*
     * Ingress procedure step 3.3
     * Wait until the water coolant tank level is < 5%...
     * Wait until the water coolant level for each EV is almost empty. 
     */
    protected void step3Task3()
    {
        info = "Wait until the water coolant level for each EV is almost empty.";

        // Get the coolant readings for each EV
        float ev1Coolant;
        float ev2Coolant;

        try
        {
            ev1Coolant = teleData.GetCoolantMl("eva1");
            ev2Coolant = teleData.GetCoolantMl("eva2");
        }
        catch
        {
            ev1Coolant = 0f;
            ev2Coolant = 0f;
        }

        // TODO check these values?
        // (instructions give in percentages, actual data is in milliliters)
        advanceFlag = (ev1Coolant < 5f &&
                        ev2Coolant < 5f);

        status = string.Format("EV-1 water coolant level: {0}\n" +
                                "EV-2 water coolant level: {1}\n",
                                helper.formatCapacity(ev1Coolant),
                                helper.formatCapacity(ev2Coolant)
                                );
    }

    /*
     * Ingress procedure step 3.4
     * Switch EV-1, EV-2 WASTE WATER to CLOSED. 
     * Close the waste water valves using the UIA. 
     */
    protected void step3Task4()
    {
        info = "Close the waste water valves using the UIA.";

        // Get the waste water valve status
        bool eva1WasteWater;
        bool eva2WasteWater;

        try
        {
            eva1WasteWater = uiaData.GetWater_Waste("eva1");
            eva2WasteWater = uiaData.GetWater_Waste("eva2");
        }

        catch
        {
            eva1WasteWater = false;
            eva2WasteWater = false;
        }

        advanceFlag = !(eva1WasteWater || eva2WasteWater);
        status = string.Format("UIA EMU-1 waste water valve status: {0}\n" +
                                "UIA EMU-1 waste water valve status: {1}",
                                helper.formatValve(eva1WasteWater),
                                helper.formatValve(eva2WasteWater)
                                );

    }

    /*
     * Ingress procedure step 4.1
     * UIA: Switch EV-1, EV-2 PWR to OFF. 
     * Prepare to disconnect from the UIA by turning off each EV’s power 
     * from the UIA side. 
     */
    protected void step4Task1()
    {
        info = "Prepare to disconnect from the UIA by turning off " +
            "each EV’s power from the UIA side.";

        // Get the power status of each suit from UIA
        bool eva1Status;
        bool eva2Status;

        try
        {
            eva1Status = uiaData.GetPower("eva1");
            eva2Status = uiaData.GetPower("eva2");
        }
        catch // for testing
        {
            eva1Status = false;
            eva2Status = false;
        }

        advanceFlag = !(eva1Status || eva2Status);
        status = string.Format("EV-1 PWR status: {0}\nEV-2 PWR status: {1}",
                                helper.formatSwitch(eva1Status),
                                helper.formatSwitch(eva2Status));

    }


}
