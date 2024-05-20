using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * This class controls the egress procedure checklist found on the egress 
 * procedure active screen. 
 * 
 * As EV-1 and EV-2 go through the egress procedure, the active task in progress
 * is shown in yellow. When the EVs are ready to proceed to the next step, 
 * the checklist item will change from yellow to green. LMCC can check on the
 * status of the current step by hovering over the checklist item; they will
 * be told when the system is ready to proceed and will be informed to click
 * the checkbox to advance to the next step. 
 * 
 */
public class EgressProcedure : Procedure
{

    // Start is called before the first frame update
    void Start()
    {

        // TODO delete this from Start function, this is just for testing
        // (Attach this to a button at some point)
        //startProcedure();

        // Make the list of task checkers
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
            step4Task1,
            step4Task2,
            step4Task3,
            step4Task4,
            step4Task5,
            step5Task1,
            step5Task2,
            step5Task3,
            step5Task4,
            step6Task1,
            step6Task2,
            step6Task3,
            step6Task4,
            step7Task1,
            step7Task2,
            step7Task3,
            step7Task4,
            step8Task1
        };

        //// Initialize the UI helper
        //helper = new ProcedureUtility();
    }

    //// Update
    //void Update()
    //{
    //    // Call the parent update function
    //    base.Update();

    //}

    /*
     * Egress procedure step 1.1
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

        catch
        {
            //Debug.Log("Exception in egress procedure step 1.1: " + e.Message);
            eva1Status = false;
            eva2Status = false;
        }

        advanceFlag = (eva1Status && eva2Status);
        status = string.Format("EV-1 PWR status: {0}\nEV-2 PWR status: {1}",
                                helper.formatSwitch(eva1Status),
                                helper.formatSwitch(eva2Status));
    }

    /*
     * Egress procedure step 1.2
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

        catch
        {
            //Debug.Log("Exception in egress procedure step 1.2: " + e.Message);
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

        catch
        {
            //Debug.Log("Exception in egress procedure step 1.3: " + e.Message);
            depressStatus = false;
        }

        advanceFlag = depressStatus;
        status = string.Format("UIA depress pump power status: {0}",
            helper.formatSwitch(depressStatus));

    }

    /*
     * Egress procedure step 2.1
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

        catch
        {
            //Debug.Log("Exception in egress procedure step 2.1: " + e.Message);
            oxyVentStatus = false;
        }

        advanceFlag = oxyVentStatus == true;
        status = string.Format("UIA O2 vent status: {0}",
            helper.formatSwitch(oxyVentStatus));
    }

    /*
     * Egress procedure step 2.2
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
     * Egress procedure step 2.3
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
     * Egress procedure step 3.1
     * Both DCU: Switch OXY to PRI.
     * For each DCU, set the primary oxygen tank as the active tank.
     */
    protected void step3Task1()
    {
        info = "For each DCU, set the primary oxygen tank as the active tank.";

        // Check the active tanks
        bool ev1Oxy;
        bool ev2Oxy;

        try
        {
            ev1Oxy = dcuData.GetOxy("eva1");
            ev2Oxy = dcuData.GetOxy("eva2");
        }
        catch
        {
            ev1Oxy = false;
            ev2Oxy = false;
        }

        advanceFlag = (ev1Oxy && ev2Oxy);
        status = string.Format("EV-1 DCU OXY active tank: {0}\n" +
                                "EV-2 DCU OXY active tank: {1}\n",
                                helper.formatDCUOxy(ev1Oxy),
                                helper.formatDCUOxy(ev2Oxy)
                                );
    }

    /*
     * Egress procedure step 3.2
     * UIA: Switch OXYGEN EMU-1, EMU-2 to OPEN.
     * Start to fill the primary oxygen tank. 
     */
    protected void step3Task2()
    {
        info = "Start to fill the primary oxygen tank for each EV.";

        // Get the oxygen readings
        bool eva1Oxy;
        bool eva2Oxy;

        try
        {
            eva1Oxy = uiaData.GetOxy("eva1");
            eva2Oxy = uiaData.GetOxy("eva2");
        }
        catch
        {
            eva1Oxy = false;
            eva2Oxy = false;
        }

        advanceFlag = (eva1Oxy && eva2Oxy);
        status = string.Format("UIA EMU-1 oxygen valve status: {0}\n" +
                                "UIA EMU-1 oxygen valve status: {1}",
                                helper.formatValve(eva1Oxy),
                                helper.formatValve(eva2Oxy)
                                );

    }

    /*
     * Egress procedure step 3.3
     * Wait until the primary O2 tank is > 3000 psi…
     * Wait until the primary oxygen tank pressure for both EVs is over 3000 psi.
     */
    protected void step3Task3()
    {
        info = "Wait until the primary oxygen tank pressure for both EVs is over 3000 psi.";

        // Get oxygen pressure readings
        float ev1Primary;
        float ev2Primary;

        try
        {
            ev1Primary = teleData.GetOxyPriPressure("eva1");
            ev2Primary = teleData.GetOxyPriPressure("eva2");
        }
        catch
        {
            ev1Primary = 0f;
            ev2Primary = 0f;
        }

        advanceFlag = (ev1Primary > 3000f &&
                        ev2Primary > 3000f);

        status = string.Format("EV-1 primary O2 tank pressure: {0}\n" +
                                "EV-2 primary O2 tank pressure: {1}\n",
                                helper.formatPressure(ev1Primary),
                                helper.formatPressure(ev2Primary)
                                );

    }

    /*
     * Egress procedure step 3.4
     * UIA: Switch OXYGEN EMU-1, EMU-2 to CLOSED.
     * Finish filling the primary oxygen tank. 
     */
    protected void step3Task4()
    {
        info = "Finish filling the primary oxygen tank for each EV.";

        // Get the oxygen readings
        bool eva1Oxy;
        bool eva2Oxy;

        try
        {
            eva1Oxy = uiaData.GetOxy("eva1");
            eva2Oxy = uiaData.GetOxy("eva2");
        }
        catch
        {
            eva1Oxy = false;
            eva2Oxy = false;
        }

        advanceFlag = !(eva1Oxy || eva2Oxy);
        status = string.Format("UIA EMU-1 oxygen valve status: {0}\n" +
                                "UIA EMU-1 oxygen valve status: {1}",
                                helper.formatValve(eva1Oxy),
                                helper.formatValve(eva2Oxy)
                                );

    }

    /*
     * Egress procedure step 4.1
     * Both DCU: Switch OXY to SEC.
     * For each DCU, set the secondary oxygen tank as the active tank.
     */
    protected void step4Task1()
    {
        info = "For each DCU, set the secondary oxygen tank as the active tank.";

        // Check the active tanks
        bool ev1Oxy;
        bool ev2Oxy;

        try
        {
            ev1Oxy = dcuData.GetOxy("eva1");
            ev2Oxy = dcuData.GetOxy("eva2");
        }
        catch
        {
            ev1Oxy = false;
            ev2Oxy = false;
        }

        advanceFlag = !(ev1Oxy && ev2Oxy);
        status = string.Format("EV-1 DCU OXY active tank: {0}\n" +
                                "EV-2 DCU OXY active tank: {1}\n",
                                helper.formatDCUOxy(ev1Oxy),
                                helper.formatDCUOxy(ev2Oxy)
                                );
    }

    /*
     * Egress procedure step 4.2
     * UIA: Switch OXYGEN EMU-1, EMU-2 to OPEN.
     * Start to fill the secondary oxygen tank. 
     */
    protected void step4Task2()
    {
        info = "Start to fill the secondary oxygen tank for each EV.";

        // Get the oxygen readings
        bool eva1Oxy;
        bool eva2Oxy;

        try
        {
            eva1Oxy = uiaData.GetOxy("eva1");
            eva2Oxy = uiaData.GetOxy("eva2");
        }
        catch
        {
            eva1Oxy = false;
            eva2Oxy = false;
        }

        advanceFlag = (eva1Oxy && eva2Oxy);
        status = string.Format("UIA EMU-1 oxygen valve status: {0}\n" +
                                "UIA EMU-1 oxygen valve status: {1}",
                                helper.formatValve(eva1Oxy),
                                helper.formatValve(eva2Oxy)
                                );
    }

    /*
     * Egress procedure step 4.3
     * Wait until the secondary O2 tank is > 3000 psi…
     * Wait until the secondary oxygen tank pressure for both EVs is over 3000 psi.
     */
    protected void step4Task3()
    {
        info = "Wait until the secondary oxygen tank pressure for both EVs is over 3000 psi.";

        // Get oxygen pressure readings
        float ev1Secondary;
        float ev2Secondary;

        try
        {
            ev1Secondary = teleData.GetOxySecPressure("eva1");
            ev2Secondary = teleData.GetOxySecPressure("eva2");
        }
        catch
        {
            ev1Secondary = 0f;
            ev2Secondary = 0f;
        }

        advanceFlag = (ev1Secondary > 3000f &&
                        ev2Secondary > 3000f);

        status = string.Format("EV-1 secondary O2 tank pressure: {0}\n" +
                                "EV-2 secondary O2 tank pressure: {1}\n",
                                helper.formatPressure(ev1Secondary),
                                helper.formatPressure(ev2Secondary)
                                );

    }

    /*
     * Egress procedure step 4.4
     * UIA: Switch OXYGEN EMU-1, EMU-2 to CLOSED.
     * Finish filling the secondary oxygen tank. 
     */
    protected void step4Task4()
    {
        info = "Finish filling the secondary oxygen tank for each EV.";

        // Get the oxygen readings
        bool eva1Oxy;
        bool eva2Oxy;

        try
        {
            eva1Oxy = uiaData.GetOxy("eva1");
            eva2Oxy = uiaData.GetOxy("eva2");
        }
        catch
        {
            eva1Oxy = false;
            eva2Oxy = false;
        }

        advanceFlag = !(eva1Oxy || eva2Oxy);
        status = string.Format("UIA EMU-1 oxygen valve status: {0}\n" +
                                "UIA EMU-1 oxygen valve status: {1}",
                                helper.formatValve(eva1Oxy),
                                helper.formatValve(eva2Oxy)
                                );


    }

    /*
     * Egress procedure step 4.5
     * Both DCU: Switch OXY to PRI.
     * For each DCU, set the primary oxygen tank as the active tank.
     */
    protected void step4Task5()
    {
        info = "For each DCU, set the primary oxygen tank as the active tank.";

        // Check the active tanks
        bool ev1Oxy;
        bool ev2Oxy;

        try
        {
            ev1Oxy = dcuData.GetOxy("eva1");
            ev2Oxy = dcuData.GetOxy("eva2");
        }
        catch
        {
            ev1Oxy = false;
            ev2Oxy = false;
        }

        advanceFlag = (ev1Oxy && ev2Oxy);
        status = string.Format("EV-1 DCU OXY active tank: {0}\n" +
                                "EV-2 DCU OXY active tank: {1}\n",
                                helper.formatDCUOxy(ev1Oxy),
                                helper.formatDCUOxy(ev2Oxy)
                                );

    }

    /*
     * Egress procedure step 5.1
     * Both DCU: Switch PUMP to OPEN.
     * For each DCU, open the water pump to allow coolant to flow between 
     * the EVs’ suits and the UIA. 
     */
    protected void step5Task1()
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
     * Egress procedure step 5.2
     * UIA: Switch EV-1, EV-2 WASTE WATER to OPEN. 
     * Turn on each EV’s waste water valve using the UIA to begin flushing 
     * the waste water. 
     */
    protected void step5Task2()
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
     * Egress procedure step 5.3
     * Wait until the water coolant tank level is < 5%...
     * Wait until the water coolant level for each EV is almost empty. 
     */
    protected void step5Task3()
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
     * Egress procedure step 5.4
     * Switch EV-1, EV-2 WASTE WATER to CLOSED. 
     * Close the waste water valves using the UIA. 
     */
    protected void step5Task4()
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
     * Egress procedure step 6.1
     * UIA: Switch EV-1, EV-2 SUPPLY WATER to OPEN.
     * Turn on each EV’s supply water valve using the UIA to begin 
     * refilling the coolant tank. 
     */
    protected void step6Task1()
    {
        info = "Turn on each EV’s supply water valve using the UIA to begin " +
            "refilling the coolant tank. ";

        // Get valve status
        bool eva1SupplyWater;
        bool eva2SupplyWater;

        try
        {
            eva1SupplyWater = uiaData.GetWater_Supply("eva1");
            eva2SupplyWater = uiaData.GetWater_Supply("eva2");
        }
        catch
        {
            eva1SupplyWater = false;
            eva2SupplyWater = false;
        }

        advanceFlag = (eva1SupplyWater && eva2SupplyWater);
        status = string.Format("UIA EMU-1 supply water valve status: {0}\n" +
                                "UIA EMU-1 supply water valve status: {1}",
                                helper.formatValve(eva1SupplyWater),
                                helper.formatValve(eva2SupplyWater)
                                );

    }

    /*
     * Egress procedure step 6.2
     * Wait until the water coolant tank level is > 95%...
     * Wait until the water coolant level for each EV is almost full. 
     */
    protected void step6Task2()
    {
        info = "Wait until the water coolant level for each EV is almost full.";

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

        advanceFlag = (ev1Coolant > 95f &&
                        ev2Coolant > 95f);

        // TODO check these values?
        // (instructions give in percentages, actual data is in milliliters)
        status = string.Format("EV-1 water coolant level: {0}\n" +
                                "EV-2 water coolant level: {1}\n",
                                helper.formatCapacity(ev1Coolant),
                                helper.formatCapacity(ev2Coolant)
                                );

    }

    /*
     * Egress procedure step 6.3
     * UIA: Switch EV-1, EV-2 SUPPLY WATER to CLOSED. 
     * Close the supply water valves using the UIA. 
     */
    protected void step6Task3()
    {
        info = "Close the supply water valves using the UIA.";

        // Get valve status
        bool eva1SupplyWater;
        bool eva2SupplyWater;

        try
        {
            eva1SupplyWater = uiaData.GetWater_Supply("eva1");
            eva2SupplyWater = uiaData.GetWater_Supply("eva2");
        }
        catch
        {
            eva1SupplyWater = false;
            eva2SupplyWater = false;
        }

        advanceFlag = !(eva1SupplyWater || eva2SupplyWater);
        status = string.Format("UIA EMU-1 supply water valve status: {0}\n" +
                                "UIA EMU-1 supply water valve status: {1}",
                                helper.formatValve(eva1SupplyWater),
                                helper.formatValve(eva2SupplyWater)
                                );

    }

    /*
     * Egress procedure step 6.4
     * Both DCU: Switch PUMP to CLOSED.
     * For each DCU, stop coolant from flowing between the EVs’ suits 
     * and the UIA.
     */
    protected void step6Task4()
    {
        info = "For each DCU, stop coolant from flowing between " +
            "the EVs’ suits and the UIA.";

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

        advanceFlag = !(eva1Pump || eva2Pump);
        status = string.Format("UIA EMU-1 water pump status: {0}\n" +
                                "UIA EMU-1 water pump status: {1}",
                                helper.formatValve(eva1Pump),
                                helper.formatValve(eva2Pump)
                                );
    }

    /*
     * Egress procedure step 7.1
     * Wait until SUIT P is at 4 psi…
     * Wait until the suit pressure for each EV is at 4 psi. 
     */
    protected void step7Task1()
    {
        info = "Wait until the suit pressure for each EV is at 4 psi.";

        // Get suit pressure readings
        float ev1SuitPressure;
        float ev2SuitPressure;

        try
        {
            // TODO check which pressure reading to use here?
            ev1SuitPressure = teleData.GetSuitPressureOxy("eva1");
            ev2SuitPressure = teleData.GetSuitPressureOxy("eva2");
        }
        catch
        {
            ev1SuitPressure = 4f;
            ev2SuitPressure = 4f;
        }


        advanceFlag = (ev1SuitPressure < 5f &&
                        ev1SuitPressure > 3f &&
                        ev2SuitPressure < 5f &&
                        ev2SuitPressure > 3f);
        status = string.Format("EV-1 suit pressure: {0}\n" +
                                "EV-2 suit pressure: {1}",
                                helper.formatPressure(ev1SuitPressure),
                                helper.formatPressure(ev2SuitPressure)
                                );

    }

    /*
     * Egress procedure step 7.2
     * UIA: Switch the DEPRESS PUMP PWR to OFF. 
     * Turn off the depress pump power to conclude the depress of each suit. 
     */
    protected void step7Task2()
    {
        info = "Turn off the depress pump power to conclude the depress of each suit.";

        // Get the UIA's depress pump status
        bool depressStatus;

        try
        {
            depressStatus = uiaData.GetDepress();
        }

        catch
        {
            depressStatus = false;
        }

        advanceFlag = !depressStatus;
        status = string.Format("UIA depress pump power status: {0}",
            helper.formatSwitch(depressStatus));

    }

    /*
     * Egress procedure step 7.3
     * Both DCU: Switch BATT to LOCAL.
     * For each DCU, switch the battery mode to local. 
     */
    protected void step7Task3()
    {
        info = "For each DCU, switch the battery mode to local.";

        // Get the current battery status of each DCU (local or umbilical)
        bool eva1Status;
        bool eva2Status;

        try
        {
            eva1Status = dcuData.GetBatt("eva1");
            eva2Status = dcuData.GetBatt("eva2");
        }

        catch
        {
            eva1Status = false;
            eva2Status = false;
        }

        advanceFlag = !(eva1Status || eva2Status);
        status = string.Format("EV-1 DCU-BATT status: {0}\nEV-2 DCU-BATT status: {1}",
                                helper.formatDCUBatt(eva1Status),
                                helper.formatDCUBatt(eva2Status));
    }

    /*
     * Egress procedure step 7.4
     * UIA: Switch EV-1, EV-2 PWR to OFF. 
     * Conclude powering each EV’s suit using the UIA. 
     */
    protected void step7Task4()
    {
        info = "Conclude powering each EV’s suit using the UIA.";

        // Get the power status of each suit from UIA
        bool eva1Status;
        bool eva2Status;

        if (uiaData != null)
        {
            eva1Status = uiaData.GetPower("eva1");
            eva2Status = uiaData.GetPower("eva2");
        }
        else // for testing
        {
            eva1Status = false;
            eva2Status = false;
        }

        advanceFlag = !(eva1Status || eva2Status);
        status = string.Format("EV-1 PWR status: {0}\nEV-2 PWR status: {1}",
                                helper.formatSwitch(eva1Status),
                                helper.formatSwitch(eva2Status));

    }

    /*
     * Egress procedure step 8.1
     * Double-check the DCU switch states for each EV.
     * Verify that the DCU switch states for each EV are at the following settings:
     *      BATT	LOCAL
     *      OXY		PRI
     *      COMMS	A
     *      FAN		PRI
     *      PUMP	CLOSED
     *      CO2		A
     */
    protected void step8Task1()
    {
        info = "Verify that both EVs are ready to conclude the egress procedure.";

        // Get the readings
        bool eva1batt;
        bool eva1oxy;
        bool eva1comms;
        bool eva1fan;
        bool eva1pump;
        bool eva1co2;

        bool eva2batt;
        bool eva2oxy;
        bool eva2comms;
        bool eva2fan;
        bool eva2pump;
        bool eva2co2;

        try
        {
            eva1batt = dcuData.GetBatt("eva1");
            eva1oxy = dcuData.GetBatt("eva1");
            eva1comms = dcuData.GetBatt("eva1");
            eva1fan = dcuData.GetBatt("eva1");
            eva1pump = dcuData.GetBatt("eva1");
            eva1co2 = dcuData.GetBatt("eva1");

            eva2batt = dcuData.GetBatt("eva2");
            eva2oxy = dcuData.GetBatt("eva2");
            eva2comms = dcuData.GetBatt("eva2");
            eva2fan = dcuData.GetBatt("eva2");
            eva2pump = dcuData.GetBatt("eva2");
            eva2co2 = dcuData.GetBatt("eva2");
        }
        catch
        {
            eva1batt = false;
            eva1oxy = false;
            eva1comms = false;
            eva1fan = false;
            eva1pump = false;
            eva1co2 = false;

            eva2batt = false;
            eva2oxy = false;
            eva2comms = false;
            eva2fan = false;
            eva2pump = false;
            eva2co2 = false;
        }

        advanceFlag = (eva1batt &&
                        eva1oxy &&
                        eva1comms &&
                        eva1fan &&
                        eva1pump &&
                        eva1co2 &&
                        eva2batt &&
                        eva2oxy &&
                        eva2comms &&
                        eva2fan &&
                        eva2pump &&
                        eva2co2);
        status = string.Format("EV-1: {0}, {1}, {2}, {3}, {4}, {5}\n" +
                                "EV-1: {6}, {7}, {8}, {9}, {10}, {11}",
                                helper.formatDCUBatt(eva1batt),
                                helper.formatDCUOxy(eva1oxy),
                                helper.formatDCUComms(eva1comms),
                                helper.formatDCUFan(eva1fan),
                                helper.formatDCUPump(eva1pump),
                                helper.formatDCUCO2(eva1co2),
                                helper.formatDCUBatt(eva2batt),
                                helper.formatDCUOxy(eva2oxy),
                                helper.formatDCUComms(eva2comms),
                                helper.formatDCUFan(eva2fan),
                                helper.formatDCUPump(eva2pump),
                                helper.formatDCUCO2(eva2co2)
                                );

    }

}
